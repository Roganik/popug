namespace popug.sharedlibs;

public interface IEventHandler
{
    public Task Handle<T>(T eventData, EventScope eventScope, IContext ctx) where T : IHasCorrelationId, new();
}