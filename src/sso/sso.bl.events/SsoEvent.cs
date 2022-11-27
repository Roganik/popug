using popug.sharedlibs;

namespace sso.bl.events;

public class SsoEvent<T> : IEvent<T>
{
    public SsoEvent(T data, string aggregateID, Guid correlationId)
    {
        var domain = PopugEventsDomainAttribute.ReadDomainFromAssemblyContainingType(typeof(SsoEvent<>));
        
        Scope = new EventScope(domain, typeof(T).Name, aggregateID);
        Data = data;
        CorrelationId = correlationId;
    }
    
    public EventScope Scope { get; }
    public T Data { get; }
    public Guid CorrelationId { get; }
}