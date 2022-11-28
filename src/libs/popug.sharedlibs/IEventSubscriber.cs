namespace popug.sharedlibs;

public interface IEventSubscriber
{
    Task Subscribe<T, TEventHandler>() where TEventHandler: IEventHandler<T>;
}