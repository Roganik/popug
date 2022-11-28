namespace popug.sharedlibs;

public interface IContext
{
    public CancellationToken CancellationToken { get; }
    public Guid CorrelationId { get; }
}