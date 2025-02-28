using Order.Reader.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace Order.Reader.Storage;

public class Storage : IStorage
{
    private readonly IConnectionMultiplexer _store;

    public Storage(IConnectionMultiplexer store)
    {
        _store = store;
    }

    public async Task<CustomerDTO?> GetCustomer(Guid id)
    {
        var db = _store.GetDatabase();

        var json = await db.StringGetAsync(id.ToString());

        if (json.HasValue)
        {
            return JsonSerializer.Deserialize<CustomerDTO>(json);
        }

        return null;
    }

    public async Task Store(CustomerDTO customer)
    {
        var db = _store.GetDatabase();

        var json = JsonSerializer.Serialize(customer);
        await db.StringSetAsync(customer.Id.ToString(), json);
    }
}
