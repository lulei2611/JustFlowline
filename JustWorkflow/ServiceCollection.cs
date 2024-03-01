using Autofac;
using Autofac.Builder;
using JustFlowline.Extensions;
using System;

namespace JustFlowline
{
    public class ServiceCollection
    {
        private readonly ContainerBuilder _containerBuilder;

        public ServiceCollection()
        {
            _containerBuilder = new ContainerBuilder();
        }

        private IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddService<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            return _containerBuilder.RegisterType<TImplementation>().As<TService>();
        }

        private IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle> AddService<TService>(TService instance) where TService : class
        {
            return _containerBuilder.RegisterInstance<TService>(instance);
        }

        private IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle> AddService<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            return _containerBuilder.Register<TService>(p => implementationFactory.Invoke(p.Resolve<IServiceProvider>()));
        }

        public void AddSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            AddService<TService, TImplementation>().SingleInstance();
        }

        public void AddSingleton<TService>(TService instance) where TService : class
        {
            AddService<TService>(instance).SingleInstance();
        }

        public void AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            AddService<TService>(p => implementationFactory.Invoke(p.GetService<IServiceProvider>())).SingleInstance();
        }

        public void AddTransient<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            AddService<TService, TImplementation>().InstancePerDependency();
        }

        public void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            AddService<TService>(p => implementationFactory.Invoke(p.GetService<IServiceProvider>())).InstancePerDependency();
        }

        public ServiceProvider Build()
        {
            var serviceProvider = new ServiceProvider(_containerBuilder);
            this.AddSingleton<IServiceProvider>(serviceProvider);
            return serviceProvider;
        }
    }
}
