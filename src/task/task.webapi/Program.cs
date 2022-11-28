using popug.sharedlibs;
using popug.webinfra;
using sso.bl.events;
using task.webapi;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.ConfigurePopugServices();

var app = builder.Build();
app.ConfigurePopugWebApplication();

app.MapGet("/", () => "Hello World!");

// todo: move to hosted service to not block other calls
// see as examples:
// - https://www.codemag.com/Article/2201061/Working-with-Apache-Kafka-in-ASP.NET-6-Core
// - https://kamil-zakiev.medium.com/kafka-consumer-api-c-117ccb514971
// - https://stackoverflow.com/questions/56733810/how-to-properly-implement-kafka-consumer-as-a-background-service-on-net-core

var subscriber = app.Services.GetService<IEventSubscriber>();
Task.Run(() => subscriber.Subscribe<UserUpdated, UserUpdatedEventHandler>()); // under the hood, consume method in synchronous

app.Run();