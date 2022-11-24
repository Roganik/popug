namespace popug.sharedlibs;

public interface IEventSubscriber
{
    Task Subscribe(EventScope eventScope, IEventHandler handler);
}