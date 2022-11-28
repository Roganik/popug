namespace popug.sharedlibs;

internal record Event<T>
{
    public required Guid CorrelationId { get; init; }
    public required T Data { get; init; }
}