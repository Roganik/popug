using Microsoft.Extensions.DependencyInjection;
using Popug.SharedLibs.Impl;
using Popug.SharedLibs.Jwt;

namespace Popug.SharedLibs;

public static class DependencyConfig
{
    public static IServiceCollection RegisterLibs(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IEventBus), typeof(EventBus));
        services.AddSingleton(typeof(IEventSubscriber), typeof(EventSubscriber));
        services.AddSingleton(typeof(IJwtTokenGenerator), typeof(JwtTokenService));
        services.AddSingleton(typeof(IJwtTokenValidator), typeof(JwtTokenService));
        return services;
    }
}