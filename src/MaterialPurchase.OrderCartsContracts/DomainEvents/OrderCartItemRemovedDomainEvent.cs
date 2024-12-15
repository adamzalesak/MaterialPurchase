using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartItemRemovedDomainEvent : DomainEvent, IOrderCartDomainEvent
{
    public override AggregateType AggregateType => AggregateType.OrderCart;
    public Guid OrderCartItemId { get; init; }
    public int ProductId { get; init; }
    public int OriginalQuantity { get; init; }
    public Guid OfferId { get; init; }
}