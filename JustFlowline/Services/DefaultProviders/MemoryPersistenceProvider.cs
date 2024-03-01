using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Services.DefaultProviders
{
    public class MemoryPersistenceProvider : IPersistenceProvider
    {
        private readonly List<FlowlineInstance> _instances = new List<FlowlineInstance>();
        private readonly List<FlowlineSubscription> _subscriptions = new List<FlowlineSubscription>();

        public async Task<string> CreateNewFlowline(FlowlineInstance flowline)
        {
            lock (_instances)
            {
                flowline.Id = Guid.NewGuid().ToString();
                _instances.Add(flowline);
                return flowline.Id;
            }
        }

        public async Task<FlowlineInstance> GetFlowline(string flowlineId, CancellationToken cancellationToken = default)
        {
            var flowline = _instances.FirstOrDefault(p => p.Id == flowlineId);
            return flowline;
        }

        public async Task PersistFlowline(FlowlineInstance flowline, CancellationToken cancellationToken = default)
        {
            lock (_instances)
            {
                var existing = _instances.First(x => x.Id == flowline.Id);
                _instances.Remove(existing);
                _instances.Add(flowline);
            }
        }

        public async Task PersistFlowline(FlowlineInstance flowline, List<FlowlineSubscription> subscriptions, CancellationToken cancellationToken = default)
        {
            lock (_instances)
            {
                var existing = _instances.First(x => x.Id == flowline.Id);
                _instances.Remove(existing);
                _instances.Add(flowline);

                lock (_subscriptions)
                {
                    if (subscriptions != null)
                    {
                        foreach (var subscription in subscriptions)
                        {
                            subscription.Id = Guid.NewGuid().ToString();
                            _subscriptions.Add(subscription);
                        }
                    }
                }
            }
        }
    }
}
