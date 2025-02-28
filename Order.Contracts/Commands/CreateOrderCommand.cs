namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommand
    {
        public static string Topic = "commands.create-order";

        public long? OrderId { get; init; }
        public string Description { get; init; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    }
}