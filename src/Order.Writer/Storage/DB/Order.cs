namespace Order.Writer.Storage.DB
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Created = 1,
        Paid = 2
    }
}
