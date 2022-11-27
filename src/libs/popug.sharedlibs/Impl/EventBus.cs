using Confluent.Kafka;
using System.Text.Json;

namespace popug.sharedlibs.Impl;

public class EventBus : IEventBus, IDisposable
{
    private readonly List<KeyValuePair<string, string>> _config = new()
    {
        new ("bootstrap.servers", "localhost:9092"),
    };

    private readonly IProducer<string, string> _eventProducer;

    public EventBus()
    {
        var builder = new ProducerBuilder<string, string>(_config);
        _eventProducer = builder.Build();
    }
    
    public void Dispose()
    {
        _eventProducer.Flush(TimeSpan.FromSeconds(10));
        _eventProducer.Dispose();
    }
    
    public Task Send<T>(IEvent<T> @event, IContext ctx)
    {
        var topic = @event.Scope.Domain+"_"+@event.Scope.Event;
        var key = @event.Scope.AggregateId;
        var value = JsonSerializer.Serialize<T>(@event.Data);
        
        var message = new Message<string, string>() {Key = key, Value = value};
        
        var job = _eventProducer
            .ProduceAsync(topic, message, ctx.CancellationToken)
            .ContinueWith(async (result) =>
            {
                var deliveryReport = await result;
                Console.WriteLine(
                    $"Delivery status: {deliveryReport.Status.ToString()}, delivered to partition {deliveryReport.Partition.Value.ToString()}");
            });
        return job;
    }
}