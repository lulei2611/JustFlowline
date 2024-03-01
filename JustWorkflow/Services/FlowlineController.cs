using JustFlowline.Exceptions;
using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Models.Events;
using System;
using System.Threading.Tasks;

namespace JustFlowline.Services
{
    public class FlowlineController : IFlowlineController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFlowlineRegistry _registry;
        private readonly IExecutionPointerFactory _pointerFactory;
        private readonly IPersistenceProvider _persistenceProvider;
        private readonly IQueueProvider _queueProvider;
        private readonly IFlowlineCycleEventHub _eventHub;

        public FlowlineController(IServiceProvider serviceProvider, IFlowlineRegistry registry, IExecutionPointerFactory pointerFactory, IPersistenceProvider persistenceProvider, IQueueProvider queueProvider, IFlowlineCycleEventHub eventHub)
        {
            _serviceProvider = serviceProvider;
            _registry = registry;
            _pointerFactory = pointerFactory;
            _persistenceProvider = persistenceProvider;
            _queueProvider = queueProvider;
            _eventHub = eventHub;
        }

        public void RegisterFlowline<TFlowline>() where TFlowline : IFlowline
        {
            RegisterFlowline<TFlowline, object>();
        }

        public void RegisterFlowline<TFlowline, TData>()
            where TFlowline : IFlowline<TData>
            where TData : new()
        {
            var fl = ActivatorUtilities.CreateInstance<TFlowline>(_serviceProvider);
            _registry.RegisterFlowline(fl);
        }

        public Task<string> StartFlowline(string id, Version version, object data, string reference = null)
        {
            return StartFlowline<object>(id, version, data, reference);
        }

        public Task<string> StartFlowline(string id, object data, string reference = null)
        {
            return StartFlowline(id, default(Version), data, reference);
        }

        public async Task<string> StartFlowline<TData>(string id, Version version, TData data = null, string reference = null) where TData : class, new()
        {
            var definition = _registry.GetDefinition(id, version);
            if (definition == null)
            {
                throw new FlowlineNotRegisteredException(id, version);
            }

            var flowline = new FlowlineInstance
            {
                FlowlineId = id,
                Version = version,
                Data = data,
                Description = definition.Description,
                NextExecution = 0,
                CreateTime = DateTime.Now,
                Status = FlowlineStatus.Runnable,
                Reference = reference,
            };

            if ((definition.DataType != null) && (data == null))
            {
                if (typeof(TData) == definition.DataType)
                    flowline.Data = new TData();
                else
                    flowline.Data = definition.DataType.GetConstructor(new Type[0]).Invoke(new object[0]);
            }
            flowline.ExecutionPointers.Add(_pointerFactory.CreateGenesisPointer(definition));

            string instId = await _persistenceProvider.CreateNewFlowline(flowline);
            await _queueProvider.QueueWork(instId, QueueType.Flowline);
            await _eventHub.PublishNotification(new FlowlineStartEvent
            {
                FlowlineId = flowline.FlowlineId,
                FlowlineInstanceId = instId,
                FlowlineVersion = version,
                EventTime = DateTime.Now,
                Reference = reference,
            });
            return instId;
        }
    }
}
