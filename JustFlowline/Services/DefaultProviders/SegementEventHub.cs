using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustFlowline.Services.DefaultProviders
{
    public class SegementEventHub : IFlowlineCycleEventHub
    {
        private readonly ILogger _logger;
        private readonly List<Action<FlowlineCycleEvent>> _subscribers;

        public SegementEventHub(ILogger logger)
        {
            _subscribers = new List<Action<FlowlineCycleEvent>>();
            _logger = logger;
        }

        public Task PublishNotification(FlowlineCycleEvent cycleEvent)
        {
            Task.Run(() =>
            {
                foreach (var subscriber in _subscribers)
                {
                    try
                    {
                        subscriber(cycleEvent);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(default(EventId), ex, $"订阅消息执行失败: {ex.Message}");
                    }
                }
            });
            return Task.CompletedTask;
        }

        public Task Start()
        {
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _subscribers.Clear();
            return Task.CompletedTask;
        }

        public void Subscribe(Action<FlowlineCycleEvent> action)
        {
            _subscribers.Add(action);
        }
    }
}
