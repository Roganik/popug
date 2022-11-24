using Microsoft.Extensions.DependencyInjection;

namespace popug.jwt;

public static class DependencyConfig
{
    public static IServiceCollection RegisterJwt(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IJwtTokenGenerator), typeof(JwtTokenService));
        services.AddSingleton(typeof(IJwtTokenValidator), typeof(JwtTokenService));
        return services;
    }
}