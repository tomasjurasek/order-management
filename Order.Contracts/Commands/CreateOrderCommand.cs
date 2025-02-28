using Order.Contracts;
using Order.Contracts.DTO;

namespace Order.Writer.CommandHandlers;

public class CreateOrderCommand : OrderMessage
{
    public static string Topic = "commands.create-order";
    public string Description { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public List<OrderItem> OrderItems { get; init; }
    public Dictionary<string, string> Properties { get; init; }
}