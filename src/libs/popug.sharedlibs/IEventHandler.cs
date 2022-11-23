namespace Popug.SharedLibs;

public interface IEventHandler
{
    public Task Handle<T>(T eventData, EventScope eventScope, IContext ctx) where T : IHasCorrelationId, new();
}