using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrdersContracts.DomainEvents;

public record OrderCreatedDomainEvent(Guid OrderId) : DomainEvent
{
    public override AggregateType AggregateType => AggregateType.Order;
}