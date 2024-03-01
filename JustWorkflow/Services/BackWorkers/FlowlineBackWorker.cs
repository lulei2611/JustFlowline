using JustFlowline.Abstrations;
using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Services.DefaultProviders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Services.BackWorkers
{
    public class FlowlineBackWorker : QueueWorker, IBackWorker
    {
        private readonly IPersistenceProvider _persistenceProvider;
        private readonly IFlowlineExecutor _flowlineExecutor;
        private readonly ILogger _logger;

        public FlowlineBackWorker(IPersistenceProvider persistenceProvider, IFlowlineExecutor flowlineExecutor, IQueueProvider queueProvider, ILogger logger) : base(queueProvider)
        {
            _persistenceProvider = persistenceProvider;
            _flowlineExecutor = flowlineExecutor;
            _logger = logger;
        }

        public override QueueType QueueType => QueueType.Flowline;

        public override async Task Execute(string item, CancellationToken cancellationToken)
        {
            FlowlineInstance flowline = null;
            FlowlineExecutionResult result = null;
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                flowline = await _persistenceProvider.GetFlowline(item, cancellationToken);

                if (flowline.Status == FlowlineStatus.Runnable)
                {
                    try
                    {
                        result = await _flowlineExecutor.Execute(flowline, cancellationToken);
                    }
                    finally
                    {
                        await _persistenceProvider.PersistFlowline(flowline, result?.Subscriptions, cancellationToken);
                    }
                }
            }
            finally
            {
                if (flowline != null)
                {
                    if ((flowline.Status == FlowlineStatus.Runnable) && flowline.NextExecution.HasValue)
                    {
                        if (flowline.NextExecution.Value < DateTime.Now.Ticks)
                        {
                            new Task(() => FutureQueue(flowline, cancellationToken)).Start();
                        }
                    }
                }
            }
        }

        private async void FutureQueue(FlowlineInstance flowline, CancellationToken cancellationToken)
        {
            try
            {
                if (!flowline.NextExecution.HasValue)
                {
                    return;
                }

                var target = (flowline.NextExecution.Value - DateTime.Now.Ticks);
                if (target > 0)
                {
                    await Task.Delay(TimeSpan.FromTicks(target), cancellationToken);
                }

                await QueueProvider.QueueWork(flowline.Id, QueueType.Flowline);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
