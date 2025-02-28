using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Events;
using Order.Writer.CommandHandlers;
using Order.Writer.Storage.DB;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("order-writer-db"));
});


builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    x.AddEntityFrameworkOutbox<OrderDbContext>(outbox =>
    {
        outbox.UseSqlServer();
        outbox.UseBusOutbox();
    });
    x.AddRider(rider =>
    {
        rider.AddConsumer<CreateOrderCommandHandler>();
        rider.AddProducer<OrderCreatedEvent>(OrderCreatedEvent.Topic);
        rider.UsingKafka((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("messaging"));

            cfg.TopicEndpoint<string, CreateOrderCommand>(CreateOrderCommand.Topic, "order-writer", e =>
            {
                e.ConfigureConsumer<CreateOrderCommandHandler>(context);
            });
        });

    });

});

var app = builder.Build();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<OrderDbContext>();
context.Database.Migrate();


app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/test", () =>
{
    return Results.Ok();
});

await app.RunAsync();


