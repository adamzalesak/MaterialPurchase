using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartFinishedDomainEvent : DomainEvent, IOrderCartDomainEvent
{
    public override AggregateType AggregateType => AggregateType.OrderCart;
}