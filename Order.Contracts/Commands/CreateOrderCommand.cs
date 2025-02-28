using Order.Contracts;

namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommand : OrderMessage
    {
        public static string Topic = "commands.create-order";
        public string Description { get; init; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    }
}