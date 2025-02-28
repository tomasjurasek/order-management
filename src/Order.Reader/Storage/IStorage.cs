using Order.Reader.Model;

namespace Order.Reader.Storage
{
    public interface IStorage
    {
        Task<CustomerDTO?> GetCustomer(Guid id);
        Task Store(CustomerDTO customer);
    }


}
