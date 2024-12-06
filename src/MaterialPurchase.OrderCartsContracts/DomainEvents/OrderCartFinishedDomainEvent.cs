using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartFinishedDomainEvent(Guid OrderCartId) : IDomainEvent, IOrderCartDomainEvent
{
    public int Version { get; set; }
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public Guid AggregateId { get; } = OrderCartId;
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}