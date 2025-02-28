using MassTransit;
using Order.Contracts.Events;
using Order.Writer.Storage.DB;

namespace Order.Writer.CommandHandlers;

public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
{
    private readonly OrderDbContext _orderDbContext;

    public CreateOrderCommandHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<CreateOrderCommand> context)
    {
        var order = new Storage.DB.Order
        {
            Id = context.Message.OrderId,
            Description =
            context.Message.Description
        };

        _orderDbContext.Orders.Add(order);

        // Publish an event. With the outbox pattern,
        // this message is stored and only dispatched after the DB transaction commits.
        await context.Publish(new OrderCreatedEvent
        {
            OrderId = order.Id,
            CreatedAt = DateTimeOffset.UtcNow,
            Description = order.Description
        });

        // Commit the changes. Only after this commit will the outbox dispatch the event.
        await _orderDbContext.SaveChangesAsync();
    }
}
