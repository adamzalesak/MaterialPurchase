using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartItemOrderedDomainEvent : DomainEvent, IOrderCartDomainEvent
{
    public override AggregateType AggregateType => AggregateType.OrderCart;
    public Guid OrderCartItemId { get; init; }
    public int ProductId { get; init; }
    public required string Name { get; init; }
    public Guid OfferId { get; init; }
    public int SupplierId { get; init; }
    public int Quantity { get; init; }
    public required Money Price { get; init; }
}