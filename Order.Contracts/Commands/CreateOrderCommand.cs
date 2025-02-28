namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommand
    {
        public static string Topic = "commands.create-order";

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}