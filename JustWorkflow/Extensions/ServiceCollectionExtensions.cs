using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Services;
using JustFlowline.Services.BackWorkers;
using JustFlowline.Services.DefaultProviders;
using System;

namespace JustFlowline.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddFlowline(this ServiceCollection services, Action<FlowlineOptions> setup = null)
        {
            var options = new FlowlineOptions(services);
            setup?.Invoke(options);
            services.AddSingleton<IFlowlineRegistry, FlowlineRegistry>();
            services.AddSingleton<IFlowlineController, FlowlineController>();
            services.AddSingleton<IFlowlineHost, FlowlineHost>();
            services.AddSingleton<IFlowlineCycleEventPublisher, FlowlineCycleEventPublisher>();
            services.AddSingleton<FlowlineOptions>(options);

            services.AddTransient<IFlowlineBuilder, FlowlineBuilder>();
            services.AddTransient<IExecutionResultProcessor, ExecutionResultProcessor>();
            services.AddTransient<IExecutionPointerFactory, ExecutionPointerFactory>();
            services.AddTransient<ILogger, Logger>();
            services.AddTransient<IBackWorker, FlowlineBackWorker>();
            services.AddTransient<IBackWorker>(sp => sp.GetService<IFlowlineCycleEventPublisher>());

            services.AddSingleton<IPersistenceProvider>(options.PersistenceFactory);
            services.AddSingleton<IQueueProvider>(options.QueueFactory);
            services.AddSingleton<IFlowlineCycleEventHub>(options.EventHubFactory);

            services.AddTransient<IFlowlineExecutor, FlowlineExecutor>();
        }
    }
}
