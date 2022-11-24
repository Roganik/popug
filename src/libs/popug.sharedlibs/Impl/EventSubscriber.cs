namespace popug.sharedlibs.Impl;

public class EventSubscriber : IEventSubscriber
{
    public Task Subscribe(EventScope eventScope, IEventHandler handler)
    {
        Console.WriteLine("Subscribing to");
        Console.WriteLine(eventScope.Event);
        return Task.CompletedTask;
    }
}