using Wolverine;

namespace MaterialPurchase.Common.Domain;

public interface IDomainEvent : IEvent
{
    AggregateType AggregateType { get; }
    Guid AggregateId { get; }
    DateTimeOffset OccurredOn { get; }
    
}