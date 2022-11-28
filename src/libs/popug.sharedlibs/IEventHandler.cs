namespace popug.sharedlibs;

public interface IEventHandler<T>
{
    public Task Handle(T eventData, IContext ctx);
}