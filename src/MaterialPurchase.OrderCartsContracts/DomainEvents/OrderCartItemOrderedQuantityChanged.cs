using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartItemOrderedQuantityChanged : IDomainEvent, IOrderCartDomainEvent
{
    public Guid OrderCartItemId { get; init; }
    public int Quantity { get; init; }
    public Guid AggregateId { get; set; }
    public Guid AggregateVersion { get; set; }
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}