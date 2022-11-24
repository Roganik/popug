namespace Popug.SharedLibs.Jwt;

public interface IJwtTokenGenerator
{
    string Generate(Guid userId, string userName, string userRole);
}