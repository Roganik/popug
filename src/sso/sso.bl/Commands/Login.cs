using Microsoft.EntityFrameworkCore;
using Popug.SharedLibs;
using sso.db;

namespace sso.bl.Commands;

public class LoginCommand
{
    public record LoginModel(string Login);

    public abstract record LoginCommandResult(bool IsSuccess);
    public record SuccessLoginCommandResult(string jwt) : LoginCommandResult(true);
    public record FailLoginCommandResult(string error) : LoginCommandResult(false);
    
    private readonly SsoDbContext _db;

    public LoginCommand(SsoDbContext db)
    {
        _db = db;
    }

    public async Task<LoginCommandResult> Execute(LoginModel m, IContext ctx)
    {
        var u = await _db.Users.FirstOrDefaultAsync(u => u.Login == m.Login, ctx.CancellationToken);
        if (u == null)
        {
            return new FailLoginCommandResult($"User {m.Login} is not found");
        }

        return new SuccessLoginCommandResult("TODO: JWT TOKEN HERE");
    }

}