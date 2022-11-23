using Microsoft.EntityFrameworkCore;
using Popug.SharedLibs;
using sso.db;

namespace sso.bl.Queries;

public class GetUsersQuery
{
    public record User(Guid Id, string Login, string FullName, string Role);

    private readonly SsoDbContext _db;

    public GetUsersQuery(SsoDbContext db)
    {
        _db = db;
    }

    public Task<List<User>> Run(IContext ctx)
    {
        var users = _db.Users
            .Select(u => new User(u.Id, u.Login, u.FullName, u.Role.ToString()))
            .ToListAsync(ctx.CancellationToken);

        return users;
    }
}