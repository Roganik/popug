namespace popug.sharedlibs.Impl;

public class EventBus : IEventBus
{
    public Task Send<T>(IEvent<T> @event, IContext ctx)
    {
        Console.WriteLine("Sending:");
        Console.WriteLine(@event.ToString());
        return Task.CompletedTask;
    }
}