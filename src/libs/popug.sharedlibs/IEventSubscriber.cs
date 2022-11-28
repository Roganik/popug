namespace popug.sharedlibs;

public interface IEventSubscriber
{
    Task Subscribe<T>(T @event, IEventHandler<T> handler);
}