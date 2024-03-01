using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Models.Events;
using System;
using System.Linq;

namespace JustFlowline.Services
{
    public class ExecutionResultProcessor : IExecutionResultProcessor
    {
        private readonly IExecutionPointerFactory _executionPointerFactory;
        private readonly IFlowlineCycleEventPublisher _eventPublisher;

        public ExecutionResultProcessor(IExecutionPointerFactory executionPointerFactory, IFlowlineCycleEventPublisher eventPublisher)
        {
            _executionPointerFactory = executionPointerFactory;
            _eventPublisher = eventPublisher;
        }

        public void HandleException(FlowlineInstance flowline, FlowlineDefinition definition, ExecutionPointer executionPointer, FlowlineUnit unit, Exception exception)
        {
            _eventPublisher.PublishNotification(new FlowlineError
            {
                EventTime = DateTime.Now,
                Reference = flowline.Reference,
                FlowlineInstanceId = flowline.Id,
                FlowlineId = flowline.FlowlineId,
                FlowlineVersion = flowline.Version,
                ExecutionPointerId = executionPointer.Id,
                UnitId = unit.Id,
                Exception = exception,
            });
            executionPointer.Status = ExecutionPointerStatus.Failed;
            executionPointer.Active = false;

            //TODO：处理节点失败后，是否重试等操作
        }

        public void ProcessExecutionResult(FlowlineInstance flowline, FlowlineDefinition definition, ExecutionPointer executionPointer, FlowlineUnit unit, UnitExecutionResult executionResult, FlowlineExecutionResult flExecutionResult)
        {
            executionPointer.Active = false;
            executionPointer.EndTime = DateTime.Now;
            executionPointer.Status = ExecutionPointerStatus.Complete;
            
            if(executionResult.Success)
            {
                foreach (var outcome in unit.Outcomes.Where(p => p.Match(executionResult, flowline.Data)))
                {
                    flowline.ExecutionPointers.Add(_executionPointerFactory.CreateNextPointer(definition, executionPointer, outcome));
                }
            }
        }
    }
}
