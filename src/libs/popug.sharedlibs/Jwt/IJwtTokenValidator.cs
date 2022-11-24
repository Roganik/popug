namespace Popug.SharedLibs.Jwt;

public interface IJwtTokenValidator
{
    bool IsValid(string token);
}