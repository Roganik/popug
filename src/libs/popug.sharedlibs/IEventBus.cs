namespace Popug.SharedLibs;

public interface IEventBus
{
    Task Send<T>(IEvent<T> @event, IContext ctx);
}