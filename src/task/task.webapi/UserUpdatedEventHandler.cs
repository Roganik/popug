using popug.sharedlibs;
using sso.bl.events;

namespace task.webapi;

public class UserUpdatedEventHandler : IEventHandler<sso.bl.events.UserUpdated>
{
    public Task Handle(UserUpdated eventData, IContext ctx)
    {
        Console.WriteLine($"Event: {eventData.Name}, {eventData.Login}");
        Console.WriteLine($"Correlation: {ctx.CorrelationId}");
        return Task.CompletedTask;
    }
}