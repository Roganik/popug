using popug.sharedlibs;
using sso.bl.events;
using sso.db;
using sso.db.Models;

namespace sso.bl.Commands;

public class CreateUserCommand
{
    private readonly SsoDbContext _db;
    private readonly IEventBus _eventBus;

    public CreateUserCommand(SsoDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }
    
    public record CreateUserModel(string Login, string FullName, string Role);
    
    public async Task Execute(CreateUserModel m, IContext ctx)
    {
        var dbUser = new db.Models.User();
        dbUser.Login = m.Login;
        dbUser.FullName = m.FullName;
        dbUser.Role = GetUserRole(m);

        _db.Users.Add(dbUser);
        await _db.SaveChangesAsync(ctx.CancellationToken);

        var e1 = new UserCreated(Id: dbUser.Id, Login: dbUser.Login, Name: dbUser.FullName);
        var e2 = new UserRoleChanged(Id: dbUser.Id, Role: dbUser.Role.ToString());
        var task1 = _eventBus.Send(e1, dbUser.Id.ToString(), ctx);
        var task2 = _eventBus.Send(e2, dbUser.Id.ToString(), ctx);

        await Task.WhenAll(task1, task2);
    }

    private static UserRole GetUserRole(CreateUserModel m)
    {
        if (m.Role.ToLower() == "admin") return UserRole.Admin;
        if (m.Role.ToLower() == "user") return UserRole.User;
        if (m.Role.ToLower() == "manager") return UserRole.Manager;
        return UserRole.Unknown;
    }
}