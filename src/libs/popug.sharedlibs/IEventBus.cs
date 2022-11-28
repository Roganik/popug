namespace popug.sharedlibs;

public interface IEventBus
{
    Task Send<T>(T @event, string aggregateId, IContext ctx);
}