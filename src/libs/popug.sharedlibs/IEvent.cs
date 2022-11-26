namespace popug.sharedlibs;

public record EventScope(string Domain, string Event, string AggregadeID);

public interface IEvent<T> : IHasCorrelationId
{
    public EventScope Scope { get; }
    public T Data { get; }
}