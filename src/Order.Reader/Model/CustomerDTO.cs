namespace Order.Reader.Model
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }

        public IList<OrderDTO> Orders { get; set; }
    }
}
