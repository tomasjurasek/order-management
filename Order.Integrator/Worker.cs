using MassTransit;
using Order.Writer.CommandHandlers;

namespace Order.Integrator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        while (!stoppingToken.IsCancellationRequested)
        {
            var producer = scope.ServiceProvider.GetRequiredService<ITopicProducer<CreateOrderCommand>>();
            await producer.Produce(new CreateOrderCommand());

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
