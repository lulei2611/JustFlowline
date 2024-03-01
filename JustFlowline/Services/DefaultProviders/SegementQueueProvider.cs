using JustFlowline.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Services.DefaultProviders
{
    public class SegementQueueProvider : IQueueProvider
    {
        private readonly Dictionary<QueueType, BlockingCollection<string>> _queues = new Dictionary<QueueType, BlockingCollection<string>>
        {
            [QueueType.Flowline] = new BlockingCollection<string>(),
            [QueueType.Index] = new BlockingCollection<string>(),
        };

        public Task QueueWork(string id, QueueType queueType)
        {
            _queues[queueType].Add(id);
            return Task.CompletedTask;
        }

        public Task<string> DequeueWork(QueueType queueType, CancellationToken cancellationToken)
        {
            if(_queues[queueType].TryTake(out var id, 100, cancellationToken))
            {
                return Task.FromResult(id);
            }
            return Task.FromResult<string>(null);
        }

        public Task Start()
        {
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            
        }
    }
}
