using MassTransit;
using Order.Contracts.Events;
using Order.Reader.Model;
using Order.Reader.Storage;

namespace Order.Reader.Handlers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IStorage _storage;

        public OrderCreatedConsumer(IStorage storage)
        {
            _storage = storage;
        }


        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var customer = await _storage.GetCustomer(context.Message.CustomerId);

            if(customer is null)
            {
                customer = new CustomerDTO
                {
                    Id = context.Message.CustomerId,
                    Orders = new List<OrderDTO>()
                    {
                        new OrderDTO
                        {
                            Id = context.Message.OrderId,
                            Description = context.Message.Description,
                            CreatedAt = context.Message.CreatedAt
                        }
                    }
                };
            }
        }
    }
}
