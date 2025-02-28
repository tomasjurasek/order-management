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
            var producer = scope.ServiceProvider.GetRequiredService<ITopicProducer<CreateOrderCommand>>();

            var zakazky = _legacyStorage.GetZakazky();
            var polozky = _legacyStorage.GetPolozky();
            foreach (var zakazka in zakazky)
            {
                var createOrderCommand = new CreateOrderCommand
                {
                    OrderId = Guid.NewGuid(),
                    OrderItems = polozky.Where(s => s.ZakazkaId == zakazka.ZakazkaId).Select(s => new Contracts.DTO.OrderItem(s.PolozkaId, 1)).ToList(),
                    Properties = new Dictionary<string, string>
                    {
                        {"legacyOrderId", zakazka.ZakazkaId.ToString()}
                    }
                };

                await producer.Produce(createOrderCommand);
                await Task.Delay(1000, stoppingToken);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
