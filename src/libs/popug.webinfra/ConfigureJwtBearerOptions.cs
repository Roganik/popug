using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using popug.jwt;

namespace popug.webinfra;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IJwtTokenValidator _jwtTokenValidator;

    public ConfigureJwtBearerOptions(IJwtTokenValidator jwtTokenValidator)
    {
        _jwtTokenValidator = jwtTokenValidator;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = _jwtTokenValidator.CreateValidationParameters();
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        Configure(options);
    }
}