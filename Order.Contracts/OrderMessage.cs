namespace Order.Contracts;

public abstract class OrderMessage
{
    public Guid OrderId { get; init; }
}
