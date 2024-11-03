using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartCreatedDomainEvent(Guid OrderCartId) : IDomainEvent
{
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public Guid AggregateId { get; } = OrderCartId;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}