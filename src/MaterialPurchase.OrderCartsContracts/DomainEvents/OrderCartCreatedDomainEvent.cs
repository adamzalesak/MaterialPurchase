using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartCreatedDomainEvent(Guid AggregateId, string Name) : IDomainEvent
{
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}