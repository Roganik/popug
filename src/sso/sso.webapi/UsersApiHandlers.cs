using Popug.SharedLibs;
using Popug.SharedLibs.Impl;
using sso.bl.Queries;
using sso.db;

namespace sso.webapi;

public static class UsersApiHandlers
{
    private static IContext CreateContext(CancellationToken cancellationToken) =>
        new Context()
        {
            CancellationToken = cancellationToken,
            CorrelationId = Guid.NewGuid(),
        };

    public static async Task<IResult> GetUsers(CancellationToken token, SsoDbContext db)
    {
        var q = new GetUsersQuery(db);
        var ctx = CreateContext(token);
        var users = await q.Run(ctx);

        return TypedResults.Ok(users);
    } 
}