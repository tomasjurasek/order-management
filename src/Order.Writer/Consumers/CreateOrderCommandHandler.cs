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
                Id = context.Message.OrderId,
                Description =
                context.Message.Description
            };

            _orderDbContext.Orders.Add(order);

            await context.Publish(new OrderCreatedEvent
            {
                OrderId = order.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                Description = order.Description
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
