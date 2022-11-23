using Microsoft.Extensions.DependencyInjection;
using Popug.SharedLibs.Impl;

namespace Popug.SharedLibs;

public static class DependencyConfig
{
    public static IServiceCollection RegisterLibs(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IEventBus), typeof(EventBus));
        services.AddSingleton(typeof(IEventSubscriber), typeof(EventSubscriber));
        return services;
    }
}