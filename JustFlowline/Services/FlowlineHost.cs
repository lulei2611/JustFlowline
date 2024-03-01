using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustFlowline.Services
{
    public class FlowlineHost : IFlowlineHost
    {
        private readonly IFlowlineController _controller;
        private readonly IFlowlineCycleEventHub _eventHub;
        private readonly IEnumerable<IBackWorker> _backWorkers;

        public IQueueProvider QueueProvider { get; private set; }

        public event OnUnitErrorHandler OnUnitError;

        public FlowlineHost(IFlowlineController controller, IFlowlineCycleEventHub eventHub, IQueueProvider queueProvider, IEnumerable<IBackWorker> backWorkers)
        {
            _controller = controller;
            _eventHub = eventHub;
            QueueProvider = queueProvider;
            _backWorkers = backWorkers;
        }

        public void Start()
        {
            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            await QueueProvider.Start();
            await _eventHub.Start();

            foreach (var worker in _backWorkers)
            {
                worker.Start();
            }
        }

        public void RegisterFlowline<TFlowline>() where TFlowline : IFlowline
        {
            _controller.RegisterFlowline<TFlowline>();
        }

        public void RegisterFlowline<TFlowline, TData>()
            where TFlowline : IFlowline<TData>
            where TData : new()
        {
            _controller.RegisterFlowline<TFlowline, TData>();
        }

        public void ReportUnitError(FlowlineInstance flowline, FlowlineUnit unit, Exception exception)
        {
            OnUnitError?.Invoke(flowline, unit, exception);
        }

        public Task<string> StartFlowline(string id, Version version, object data, string reference = null)
        {
            return _controller.StartFlowline(id, version, data, reference);
        }

        public Task<string> StartFlowline(string id, object data, string reference = null)
        {
            return StartFlowline(id, default, data, reference);
        }
    }
}
