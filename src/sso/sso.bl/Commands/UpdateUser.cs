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
        var e1 = new UserUpdated(Id: dbUser.Id, Login: dbUser.Login, Name: dbUser.FullName);
        var e2 = new UserRoleChanged(Id: dbUser.Id, Role: dbUser.Role.ToString());
        
        await _eventBus.Send(e1, dbUser.Id.ToString(), ctx);
        await _eventBus.Send(e2, dbUser.Id.ToString(), ctx);
    }

    private static UserRole GetUserRole(UpdateUserModel m)
    {
        if (m.Role.ToLower() == "admin") return UserRole.Admin;
        if (m.Role.ToLower() == "user") return UserRole.User;
        if (m.Role.ToLower() == "manager") return UserRole.Manager;
        return UserRole.Unknown;
    }
}