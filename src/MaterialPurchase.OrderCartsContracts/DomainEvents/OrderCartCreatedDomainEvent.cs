using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCartsContracts.DomainEvents;

public record OrderCartCreatedDomainEvent : DomainEvent, IOrderCartDomainEvent
{
    public OrderCartCreatedDomainEvent(Guid aggregateId, string name)
    {
        AggregateId = aggregateId;
        Name = name;
    }

    public override AggregateType AggregateType => AggregateType.OrderCart;
    public string Name { get; }

}