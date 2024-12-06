using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartCreatedDomainEvent(Guid AggregateId, string Name) : IDomainEvent, IOrderCartDomainEvent
{
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public Guid AggregateVersion { get; set; }
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}