using JustFlowline.Abstrations;
using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Services
{
    public class FlowlineExecutor : IFlowlineExecutor
    {
        private readonly IFlowlineRegistry _flowlineRegistry;
        private readonly IExecutionResultProcessor _executionResultProcessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFlowlineCycleEventPublisher _eventPublisher;
        private readonly ILogger _logger;
        private IFlowlineHost Host => _serviceProvider.GetService<IFlowlineHost>();

        public FlowlineExecutor(IFlowlineRegistry flowlineRegistry, IExecutionResultProcessor executionResultProcessor, IServiceProvider serviceProvider, ILogger logger, IFlowlineCycleEventPublisher eventPublisher)
        {
            _flowlineRegistry = flowlineRegistry;
            _executionResultProcessor = executionResultProcessor;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<FlowlineExecutionResult> Execute(FlowlineInstance flowline, CancellationToken cancellationToken)
        {
            var flResult = new FlowlineExecutionResult();

            var definition = _flowlineRegistry.GetDefinition(flowline.FlowlineId, flowline.Version);
            if(definition == null)
            {
                _logger.LogError($"流程线{flowline.Id}未注册");
                return flResult;
            }

            var executePointers = new List<ExecutionPointer>(flowline.ExecutionPointers.Where(p => p.Active));

            foreach(var executePointer in executePointers)
            {
                if (!executePointer.Active)
                {
                    continue;
                }
                var unit = definition.Units.FindById(executePointer.UnitId);
                if(unit == null)
                {
                    _logger.LogError($"没有在流程线定义中找到流程单元{executePointer.UnitId}");
                    flResult.Errors.Add(new ExecutionError()
                    {
                        ErrorTime = DateTime.Now,
                        FlowlineId = flowline.FlowlineId,
                        Message = $"没有在流程线定义中找到流程单元{executePointer.UnitId}",
                        PointerId = executePointer.Id,
                    });
                    continue;
                }

                try
                {
                    if (!InitializeUnit(flowline, definition, unit, executePointer, flResult))
                    {
                        continue;
                    }
                    await ExecuteUnit(flowline, definition, unit, executePointer, flResult);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"流程线{flowline.Id}执行到流程单元{unit.Id}时出错，错误信息：{ex.Message}");
                    flResult.Errors.Add(new ExecutionError()
                    {
                        ErrorTime = DateTime.Now,
                        FlowlineId = flowline.FlowlineId,
                        Message = ex.Message,
                        PointerId = executePointer.Id,
                    });
                    Host.ReportUnitError(flowline, unit, ex);
                    _executionResultProcessor.HandleException(flowline, definition, executePointer, unit, ex);
                }
            }

            await DetermineNextExecutionTime(flowline, definition);
            return flResult;
        }

        public bool InitializeUnit(FlowlineInstance flowline, FlowlineDefinition definition, FlowlineUnit unit, ExecutionPointer executionPointer, FlowlineExecutionResult flResult)
        {
            if (executionPointer.Status != ExecutionPointerStatus.Running)
            {
                executionPointer.Status = ExecutionPointerStatus.Running;
                _eventPublisher.PublishNotification(new UnitStartEvent
                {
                    EventTime = DateTime.Now,
                    Reference = flowline.Reference,
                    ExecutionPointerId = executionPointer.Id,
                    UnitId = unit.Id,
                    FlowlineId = flowline.FlowlineId,
                    FlowlineInstanceId = flowline.Id,
                    FlowlineVersion = flowline.Version,
                });
            }

            if (!executionPointer.StartTime.HasValue)
            {
                executionPointer.StartTime = DateTime.Now;
            }

            return true;
        }

        public async Task ExecuteUnit(FlowlineInstance flowline, FlowlineDefinition definition, FlowlineUnit unit, ExecutionPointer executionPointer, FlowlineExecutionResult flResult,CancellationToken cancellationToken = default)
        {
            var context = new UnitExecutionContext()
            {
                Flowline = flowline,
                Unit = unit,
                ExecutionPointer = executionPointer,
                PersistenceData = null,
                CancellationToken = cancellationToken,
            };
            var unitWork = unit.ConstructUnitWork(_serviceProvider);
            if(unitWork == null)
            {
                _logger.LogError($"构建流程单元失败{unit.WorkType}");
                flResult.Errors.Add(new ExecutionError
                {
                    FlowlineId = flowline.Id,
                    PointerId = executionPointer.Id,
                    ErrorTime = DateTime.Now,
                    Message = $"构建流程单元失败{unit.WorkType}",
                });
                return;
            }
            foreach(var input in unit.Inputs)
            {
                input.FeedIn(flowline.Data, unitWork);
            }
            switch (unit.BeforeExecute(context))
            {
                case ExecutionPipelineCommandResult.Defer:
                    return;
                case ExecutionPipelineCommandResult.End:
                    flowline.Status = FlowlineStatus.Complete;
                    flowline.CompleteTime = DateTime.Now;
                    return;
            }
            var executionResult = await unitWork.MakeAsync(context);
            if (executionResult.Success)
            {
                foreach(var output in unit.Outputs)
                {
                    output.FeedOut(flowline.Data, unitWork);
                }
            }

            unit.AfterExecute(context);
            _executionResultProcessor.ProcessExecutionResult(flowline, definition, executionPointer, unit, executionResult, flResult);
        }
    
        private async Task DetermineNextExecutionTime(FlowlineInstance flowline,FlowlineDefinition definition)
        {
            flowline.NextExecution = null;

            foreach (var pointer in flowline.ExecutionPointers.Where(x => x.Active))
            {
                flowline.NextExecution = 0;
                return;
            }

            if ((flowline.NextExecution != null) || (flowline.ExecutionPointers.Any(x => x.EndTime == null)))
            {
                return;
            }

            flowline.Status = FlowlineStatus.Complete;
            flowline.CompleteTime = DateTime.Now;
        }
    }
}
