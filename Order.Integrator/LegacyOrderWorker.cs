using MassTransit;
using Order.Integrator.Storage;
using Order.Writer.CommandHandlers;

namespace Order.Integrator;

public class LegacyOrderWorker : BackgroundService
{
    private readonly ILogger<LegacyOrderWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly LegacyAdapterStorage _legacyStorage;

    public LegacyOrderWorker(ILogger<LegacyOrderWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _legacyStorage = new LegacyAdapterStorage();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        while (!stoppingToken.IsCancellationRequested)
        {
            var zakazka = _legacyStorage.GetZakazky();
            var polozky = _legacyStorage.GetPolozky();

            var producer = scope.ServiceProvider.GetRequiredService<ITopicProducer<CreateOrderCommand>>();
            await producer.Produce(new CreateOrderCommand());

            await Task.Delay(5000, stoppingToken);
        }
    }
}
