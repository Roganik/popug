using Microsoft.Extensions.DependencyInjection;
using popug.sharedlibs.Impl;

namespace popug.sharedlibs;

public static class DependencyConfig
{
    public static IServiceCollection RegisterLibs(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IEventBus), typeof(EventBus));
        services.AddSingleton(typeof(IEventSubscriber), typeof(EventSubscriber));
        return services;
    }
}