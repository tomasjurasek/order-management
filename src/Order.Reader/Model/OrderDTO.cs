namespace Order.Reader.Model
{
    public class  OrderDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
