using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartItemOrderedQuantityChanged : DomainEvent, IOrderCartDomainEvent
{
    public override AggregateType AggregateType => AggregateType.OrderCart;
    public Guid OrderCartItemId { get; init; }
    public int Quantity { get; init; }
}