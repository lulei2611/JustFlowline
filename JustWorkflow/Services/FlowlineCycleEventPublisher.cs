using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace JustFlowline.Services
{
    public class FlowlineCycleEventPublisher : IFlowlineCycleEventPublisher, IDisposable
    {
        private readonly BlockingCollection<FlowlineCycleEvent> _cycleEvents;
        private Task _dispatchTask;

        public FlowlineCycleEventPublisher()
        {
            _cycleEvents = new BlockingCollection<FlowlineCycleEvent>();
        }

        public void PublishNotification(FlowlineCycleEvent cycleEvent)
        {
            if (_cycleEvents.IsAddingCompleted)
            {
                return;
            }
            _cycleEvents.Add(cycleEvent);
        }

        public void Start()
        {
            if (_dispatchTask != null)
            {
                throw new InvalidOperationException("重复启动事件发布器");
            }
            _dispatchTask = new Task(Execute);
            _dispatchTask.Start();
        }

        public void Stop()
        {
            _cycleEvents.CompleteAdding();
            _dispatchTask.Wait();
            _dispatchTask = null;
        }

        private void Execute()
        {
            foreach(var evt in _cycleEvents.GetConsumingEnumerable())
            {
            
            }
        }

        public void Dispose()
        {
            _cycleEvents.Dispose();
        }
    }
}
