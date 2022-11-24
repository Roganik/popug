using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using popug.jwt;
using popug.sharedlibs;

namespace popug.webinfra;

public static class PopugWebApplication
{
    public static IServiceCollection ConfigurePopugServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.RegisterLibs();
        services.RegisterJwt();
        
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Popug SSO API",
                Description = "An ASP.NET Core Web API for managing ToDo items",
            });
    
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
    
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            }); 
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
        services.ConfigureOptions<ConfigureJwtBearerOptions>(); // register it separatelly to use DI 
        services.AddAuthorization();

        return services;
    }

    public static WebApplication ConfigurePopugWebApplication(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        return app;
    }
    
    
}