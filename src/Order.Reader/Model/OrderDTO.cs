namespace Order.Reader.Model
{
    public class  OrderDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
