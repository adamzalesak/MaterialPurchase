using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrdersContracts.DomainEvents;

public record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent
{
    public int Version { get; set; }
    public AggregateType AggregateType { get; } = AggregateType.Order;
    public Guid AggregateId { get; } = OrderId;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}