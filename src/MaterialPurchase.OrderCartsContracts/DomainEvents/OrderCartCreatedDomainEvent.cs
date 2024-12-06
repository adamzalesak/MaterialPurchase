using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartCreatedDomainEvent(Guid AggregateId, string Name) : IDomainEvent, IOrderCartDomainEvent
{
    public int Version { get; set; }
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}