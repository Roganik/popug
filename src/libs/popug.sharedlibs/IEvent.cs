namespace Popug.SharedLibs;

public record EventScope(string Domain, string Event);

public interface IEvent<T> : IHasCorrelationId
{
    public EventScope Scope { get; }
    public T Data { get; }
}