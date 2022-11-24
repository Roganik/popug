namespace popug.sharedlibs;

public interface IHasCorrelationId
{
    public Guid CorrelationId { get; }
}

public interface IContext : IHasCorrelationId
{
    public CancellationToken CancellationToken { get; }
}