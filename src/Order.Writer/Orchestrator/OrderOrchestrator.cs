using MassTransit;
using Order.Contracts.Commands;
using Order.Contracts.Commands.Logistics;
using Order.Contracts.Events;
using Order.Contracts.Events.Logistics;
using Order.Contracts.Events.Payment;

namespace Order.Writer.Orchestrator;

public class OrderOrchestrator : MassTransitStateMachine<OrderProcessState>
{
    public Event<OrderCreatedEvent> OrderCreated { get; private set; }
    public Event<PaymentCompletedEvent> PaymentCompleted { get; private set; }
    public Event<OrderReservedEvent> OrderReserved { get; private set; }
    public Event<ZakazkaDelivered> ZakazkaDelivered { get; private set; }

    public State Created { get; private set; }
    public State Accepted { get; private set; }
    public State Reserved { get; private set; }
    public State Paid { get; private set; }
    public State Delivered { get; private set; }

    private void ConfigureCorrelationIds()
    {
        Event(() => OrderCreated, x => x.CorrelateById(c => c.Message.OrderId));
        Event(() => PaymentCompleted, x => x.CorrelateById(c => c.Message.OrderId));
        Event(() => ZakazkaDelivered, x => x.CorrelateById(c => c.Message.OrderId));
        Event(() => OrderReserved, x => x.CorrelateById(c => c.Message.OrderId));
    }

    public OrderOrchestrator()
    {
        ConfigureCorrelationIds();

        InstanceState(c => c.State);

        Initially(
            When(OrderCreated)
                .Then(context => context.Saga.OrderId = context.Message.OrderId)
                .Publish(context => new ReserveZakazkaCommand { OrderId = context.Message.OrderId })
                .TransitionTo(Accepted)
        );

        During(Accepted,
            When(PaymentCompleted)
                .Then(context => context.Saga.PaidAt = DateTimeOffset.UtcNow)
                .Publish(context => new PayOrderCommand { OrderId = context.Message.OrderId, Price = context.Message.Price })
                .TransitionTo(Paid)
        );

        During(Paid,
            When(ZakazkaDelivered)
                .Then(context => context.Saga.DeliveredAt = DateTimeOffset.UtcNow)
                .TransitionTo(Delivered)
        );
    }
}


public class OrderProcessState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }
    public string State { get; set; }
}
