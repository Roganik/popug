namespace Popug.SharedLibs.Impl;

public class Context : IContext
{
    public CancellationToken CancellationToken { get; init; }
    public Guid CorrelationId { get; init; }
}