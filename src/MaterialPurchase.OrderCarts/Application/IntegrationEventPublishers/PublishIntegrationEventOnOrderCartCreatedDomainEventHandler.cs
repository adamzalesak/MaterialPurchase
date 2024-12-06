using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCartsContracts.IntegrationEvents;

namespace MaterialPurchase.OrderCarts.Application.IntegrationEventPublishers;

public class PublishIntegrationEventOnOrderCartCreatedDomainEventHandler
{
    // handle OrderCartCreatedDomainEvent and publish new OrderCartCreatedIntegrationEvent as a cascading message
    public OrderCartCreatedIntegrationEvent Handle(OrderCartCreatedDomainEvent @event)
    {
        return new OrderCartCreatedIntegrationEvent
        {
            Id = @event.AggregateId,
            Name = @event.Name,
        };
    }
}