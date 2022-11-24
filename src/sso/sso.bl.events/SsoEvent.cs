using popug.sharedlibs;

namespace sso.bl.events;

public class SsoEvent<T> : IEvent<T>
{
    public SsoEvent(T data, Guid correlationId)
    {
        Scope = new EventScope("SSO", typeof(T).Name);
        Data = data;
        CorrelationId = correlationId;
    }
    
    public EventScope Scope { get; }
    public T Data { get; }
    public Guid CorrelationId { get; }
}