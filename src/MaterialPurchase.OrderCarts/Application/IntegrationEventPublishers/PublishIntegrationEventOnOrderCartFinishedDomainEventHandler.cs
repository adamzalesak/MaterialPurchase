using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCartsContracts.IntegrationEvents;

namespace MaterialPurchase.OrderCarts.Application.IntegrationEventPublishers;

public class PublishIntegrationEventOnOrderCartFinishedDomainEventHandler
{
    // handle OrderCartFinishedDomainEvent and publish new OrderCartFinishedIntegrationEvent as a cascading message
    public OrderCartFinishedIntegrationEvent Handle(OrderCartFinishedDomainEvent @event)
    {
        return new OrderCartFinishedIntegrationEvent
        {
            Id = @event.AggregateId,
        };
    }
}