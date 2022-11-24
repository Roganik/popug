using Microsoft.IdentityModel.Tokens;

namespace popug.jwt;

public interface IJwtTokenValidator
{
    bool IsValid(string token);
    public TokenValidationParameters CreateValidationParameters();
}