using Microsoft.EntityFrameworkCore;
using Popug.SharedLibs;
using Popug.SharedLibs.Jwt;
using sso.db;

namespace sso.bl.Commands;

public class LoginCommand
{
    public record LoginModel(string Login);

    public abstract record LoginCommandResult(bool IsSuccess);
    public record SuccessLoginCommandResult(string jwt) : LoginCommandResult(true);
    public record FailLoginCommandResult(string error) : LoginCommandResult(false);
    
    private readonly SsoDbContext _db;
    private readonly IJwtTokenGenerator _jwtGenerator;

    public LoginCommand(SsoDbContext db, IJwtTokenGenerator jwtGenerator)
    {
        _db = db;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<LoginCommandResult> Execute(LoginModel m, IContext ctx)
    {
        var u = await _db.Users.FirstOrDefaultAsync(u => u.Login == m.Login, ctx.CancellationToken);
        if (u == null)
        {
            return new FailLoginCommandResult($"User {m.Login} is not found");
        }

        var token = _jwtGenerator.Generate(u.Id, u.FullName, u.Role.ToString());
        return new SuccessLoginCommandResult(token);
    }

}