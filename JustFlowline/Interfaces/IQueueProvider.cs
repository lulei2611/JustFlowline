using System;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IQueueProvider : IDisposable
    {
        Task QueueWork(string id, QueueType queueType);

        Task<string> DequeueWork(QueueType queueType, CancellationToken cancellationToken);

        Task Start();

        Task Stop();
    }

    public enum QueueType
    {
        Flowline,

        Index,
    }
}
