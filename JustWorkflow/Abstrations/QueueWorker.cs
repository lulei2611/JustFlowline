using JustFlowline.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Abstrations
{
    public abstract class QueueWorker : IBackWorker
    {
        public abstract QueueType QueueType { get; }
        private CancellationTokenSource _cancellationTokenSource;
        protected Task DispatchTask; 
        protected readonly IQueueProvider QueueProvider;

        public QueueWorker(IQueueProvider queueProvider)
        {
            this.QueueProvider = queueProvider;
        }

        public abstract Task Execute(string item, CancellationToken cancellationToken);

        public virtual void Start()
        {
            if (DispatchTask != null)
            {
                throw new InvalidOperationException("任务重复启动");
            }
            _cancellationTokenSource = new CancellationTokenSource();
            DispatchTask = Task.Factory.StartNew(Execute, TaskCreationOptions.LongRunning);
        }

        public virtual void Stop()
        {
            _cancellationTokenSource.Cancel();
            if(DispatchTask != null)
            {
                DispatchTask.Wait();
                DispatchTask = null;
            }
        }

        public async Task Execute()
        {
            var cancellationToken = _cancellationTokenSource.Token;
            while(!cancellationToken.IsCancellationRequested)
            {
                var item = await this.QueueProvider.DequeueWork(this.QueueType, cancellationToken);
                if (item == null)
                {
                    continue;
                }
                Execute(item, cancellationToken).Wait();
            }
        }
    }
}
