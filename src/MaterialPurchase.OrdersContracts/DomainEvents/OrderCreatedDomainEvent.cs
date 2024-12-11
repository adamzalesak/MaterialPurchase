using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrdersContracts.DomainEvents;

public record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent
{
    public AggregateType AggregateType { get; } = AggregateType.Order;
    public Guid AggregateId { get; set; } = OrderId;
    public Guid AggregateVersion { get; set; }
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}