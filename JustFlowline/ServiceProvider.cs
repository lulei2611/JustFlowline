using Autofac;
using System;

namespace JustFlowline
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly ContainerBuilder _containerBuilder;
        private IContainer _container;

        public ServiceProvider(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public object GetService(Type serviceType)
        {
            Build();
            return _container.Resolve(serviceType);
        }

        public TService GetService<TService>()
        {
            Build();
            TService service = _container.Resolve<TService>();
            return service;
        }

        private void Build()
        {
            if (_container == null)
            {
                _container = _containerBuilder.Build();
            }
        }
    }
}
