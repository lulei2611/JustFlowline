using JustFlowline.Models;
using System;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineCycleEventHub
    {
        Task PublishNotification(FlowlineCycleEvent cycleEvent);

        void Subscribe(Action<FlowlineCycleEvent> action);

        Task Start();

        Task Stop();
    }
}
