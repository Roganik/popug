using Microsoft.IdentityModel.Tokens;

namespace Popug.SharedLibs.Jwt;

public interface IJwtTokenValidator
{
    bool IsValid(string token);
    public TokenValidationParameters CreateValidationParameters();
}