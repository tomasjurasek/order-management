namespace Order.Contracts.Events
{
    public class OrderCreatedEvent
    {
        public static string Topic = "events.order-created";
        public long OrderId { get; init; }
        public string Description { get; init; }
        public Guid CustomerId { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}
