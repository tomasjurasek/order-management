using MassTransit;
using Order.Integrator;
using Order.Writer.CommandHandlers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    x.AddRider(rider =>
    {
        rider.AddProducer<CreateOrderCommand>(CreateOrderCommand.Topic);

        rider.UsingKafka((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("messaging"));
        });

    });

});

var host = builder.Build();
await host.Services.GetRequiredService<IBusControl>().StartAsync();
await host.RunAsync();
