using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartFinishedDomainEvent(Guid OrderCartId) : IDomainEvent, IOrderCartDomainEvent
{
    public AggregateType AggregateType { get; } = AggregateType.OrderCart;
    public Guid AggregateId { get; set; } = OrderCartId;
    public Guid AggregateVersion { get; set; }
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}