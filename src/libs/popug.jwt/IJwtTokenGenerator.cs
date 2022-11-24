namespace popug.jwt;

public interface IJwtTokenGenerator
{
    string Generate(Guid userId, string userName, string userRole);
}