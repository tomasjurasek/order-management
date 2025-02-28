namespace Order.Contracts.Commands
{
    public class PayOrderCommand : OrderMessage
    {
        public static string Topic = "commands.pay-order";
        public decimal Price { get; init; }
    }
}
