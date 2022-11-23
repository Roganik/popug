namespace Popug.SharedLibs;

public interface IEventSubscriber
{
    Task Subscribe(EventScope eventScope, IEventHandler handler);
}