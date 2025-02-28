namespace Order.Contracts.Events
{
    public class OrderCreatedEvent : OrderMessage
    {
        public static string Topic = "events.order-created";
        public string Description { get; init; }
        public Guid CustomerId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
