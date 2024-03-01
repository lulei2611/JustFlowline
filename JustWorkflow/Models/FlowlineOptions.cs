using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Services.DefaultProviders;
using System;

namespace JustFlowline.Models
{
    public class FlowlineOptions
    {
        private readonly ServiceCollection _serviceCollection;

        public Func<IServiceProvider,IPersistenceProvider> PersistenceFactory { get; set; }
        public Func<IServiceProvider,IQueueProvider> QueueFactory { get; set; }
        public Func<IServiceProvider, IFlowlineCycleEventHub> EventHubFactory {  get; set; }

        public FlowlineOptions(ServiceCollection serviceCollection) 
        {
            _serviceCollection = serviceCollection;
            this.PersistenceFactory = (sp) => new MemoryPersistenceProvider();
            this.QueueFactory = (sp) => new SegementQueueProvider();
            this.EventHubFactory = (sp) => new SegementEventHub(sp.GetService<ILogger>());
        }
    }
}
