using Confluent.Kafka;

namespace popug.sharedlibs.Impl;

public class EventSubscriber : IEventSubscriber
{
    private readonly List<KeyValuePair<string, string>> _config = new()
    {
        new ("bootstrap.servers", "localhost:9092"),
        new ("group.id", "kafka-dotnet-getting-started"),
        new ("auto.offset.reset", "earliest"),
    };
    
    public Task Subscribe(EventScope eventScope, IEventHandler handler)
    {
        using (var consumer = new ConsumerBuilder<string, string>(_config.AsEnumerable()).Build())
        {
            var topic = eventScope.Domain+"_"+eventScope.Event;
            consumer.Subscribe(topic);
            try {
                while (true) {
                    var cr = consumer.Consume();
                    Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException) {
                // Ctrl-C was pressed.
            }
            finally{
                consumer.Close();
            }
        }
        return Task.CompletedTask;
    }
}