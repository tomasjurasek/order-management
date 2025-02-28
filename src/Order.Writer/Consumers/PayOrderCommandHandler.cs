using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Commands;
using Order.Writer.Storage.DB;

namespace Order.Writer.Consumers
{
    public class PayOrderCommandHandler : IConsumer<PayOrderCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public PayOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task Consume(ConsumeContext<PayOrderCommand> context)
        {
            var order = await _orderDbContext.Orders.FirstAsync(s => s.Id == context.Message.OrderId);
            order.Status = OrderStatus.Paid;
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
