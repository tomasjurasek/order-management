namespace Order.Writer.CommandHandlers
{
    public class CreateOrderCommand
    {
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}