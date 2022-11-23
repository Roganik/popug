using Popug.SharedLibs;
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

        var e1 = UserCreatedEvent(dbUser, ctx);
        var e2 = UserRoleChangedEvent(dbUser, ctx);
        var task1 = _eventBus.Send(e1, ctx);
        var task2 = _eventBus.Send(e2, ctx);

        await Task.WhenAll(task1, task2);
    }

    private IEvent<UserCreated> UserCreatedEvent(db.Models.User u, IContext ctx)
    {
        var data = new UserCreated(Id: u.Id, Login: u.Login, Name: u.FullName);
        var e = new SsoEvent<UserCreated>(data, ctx.CorrelationId);

        return e;
    }
    
    private IEvent<UserRoleChanged> UserRoleChangedEvent(db.Models.User u, IContext ctx)
    {
        var data = new UserRoleChanged(Id: u.Id, Role: u.Role.ToString());
        var e = new SsoEvent<UserRoleChanged>(data, ctx.CorrelationId);

        return e;
    }

    private static UserRole GetUserRole(CreateUserModel m)
    {
        if (m.Role.ToLower() == "admin") return UserRole.Admin;
        if (m.Role.ToLower() == "user") return UserRole.User;
        if (m.Role.ToLower() == "manager") return UserRole.Manager;
        return UserRole.Unknown;
    }
}