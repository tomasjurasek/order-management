using MassTransit;
using Order.Contracts.Events;
using Order.Reader.Handlers;
using Order.Reader.Storage;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<IStorage, Storage>();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("order-reader-db")));

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    x.AddRider(rider =>
    {
        rider.AddConsumer<OrderCreatedConsumer>();
        rider.UsingKafka((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("messaging"));

            cfg.TopicEndpoint<string, OrderCreatedEvent>(OrderCreatedEvent.Topic, "order-reader", e =>
            {
                e.ConfigureConsumer<OrderCreatedConsumer>(context);
            });
        });

    });

});


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/customers/{Id:guid}", async(Guid Id, IStorage storage) =>
{
    return await storage.GetCustomer(Id);
});

app.Run();

