using MassTransit;
using Order.Contracts.Events;
using Order.Writer.Storage.DB;

namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            var lastOrder = _orderDbContext.Orders.LastOrDefault();
            var order = new Storage.DB.Order
            {
                OrderId = context.Message.OrderId ?? lastOrder?.OrderId + 1 ?? 1,
                Description =
                context.Message.Description
            };

            _orderDbContext.Orders.Add(order);

            await context.Publish(new OrderCreatedEvent
            {
                OrderId = order.OrderId,
                CreatedAt = DateTimeOffset.UtcNow,
                Description = order.Description
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
