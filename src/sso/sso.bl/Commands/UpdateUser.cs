using Microsoft.EntityFrameworkCore;
using popug.sharedlibs;
using sso.bl.events;
using sso.db;
using sso.db.Models;

namespace sso.bl.Commands;

public class UpdateUserCommand
{
    private readonly SsoDbContext _db;
    private readonly IEventBus _eventBus;

    public UpdateUserCommand(SsoDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }
    
    public record UpdateUserModel(Guid userId, string Login, string FullName, string Role);
    
    public async Task Execute(UpdateUserModel m, IContext ctx)
    {
        var dbUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == m.userId);
        if (dbUser == null) return;
        
        dbUser.Login = m.Login;
        dbUser.FullName = m.FullName;
        dbUser.Role = GetUserRole(m);
        
        await _db.SaveChangesAsync(ctx.CancellationToken);
        await SendUserUpdatedEvent(dbUser, ctx);
        await SendUserRoleChangedEvent(dbUser, ctx);
    }

    private Task SendUserUpdatedEvent(db.Models.User u, IContext ctx)
    {
        var data = new UserUpdated(Id: u.Id, Login: u.Login, Name: u.FullName);
        var e = new SsoEvent<UserUpdated>(data, u.Id.ToString(), ctx.CorrelationId);
        
        return _eventBus.Send(e, ctx);
    }
    
    private Task SendUserRoleChangedEvent(db.Models.User u, IContext ctx)
    {
        var data = new UserRoleChanged(Id: u.Id, Role: u.Role.ToString());
        var e = new SsoEvent<UserRoleChanged>(data, u.Id.ToString(), ctx.CorrelationId);
        
        return _eventBus.Send(e, ctx);
    } 
    private static UserRole GetUserRole(UpdateUserModel m)
    {
        if (m.Role.ToLower() == "admin") return UserRole.Admin;
        if (m.Role.ToLower() == "user") return UserRole.User;
        if (m.Role.ToLower() == "manager") return UserRole.Manager;
        return UserRole.Unknown;
    }
}