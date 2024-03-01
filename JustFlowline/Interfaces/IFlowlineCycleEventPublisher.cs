using JustFlowline.Models;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineCycleEventPublisher : IBackWorker
    {
        void PublishNotification(FlowlineCycleEvent cycleEvent);
    }
}
