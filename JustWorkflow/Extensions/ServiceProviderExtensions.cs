using System;

namespace JustFlowline.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider) where TService : class
        {
            var obj = serviceProvider.GetService(typeof(TService));
            if(obj is TService service)
            {
                return service;
            }
            return null;
        }
    }
}
