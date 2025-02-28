namespace Order.Contracts.Events.Payment;

public class PaymentCompletedEvent : OrderMessage
{
    public static string Topic = "events.payment-completed";
    public Guid Id { get; init; }
    public decimal Price { get; init; }
}
