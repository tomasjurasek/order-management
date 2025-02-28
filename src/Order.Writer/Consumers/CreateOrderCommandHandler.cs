using MassTransit;

namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        public Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            return Task.CompletedTask;
        }
    }
}
