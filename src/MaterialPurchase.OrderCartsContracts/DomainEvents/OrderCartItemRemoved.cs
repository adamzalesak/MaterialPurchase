using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartItemRemoved : DomainEvent, IOrderCartDomainEvent
{
    public override AggregateType AggregateType => AggregateType.OrderCart;
    public Guid OrderCartItemId { get; init; }
}