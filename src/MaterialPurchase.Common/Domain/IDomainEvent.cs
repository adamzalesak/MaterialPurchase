namespace MaterialPurchase.Common.Domain;

public interface IDomainEvent
{
    AggregateType AggregateType { get; }
    Guid AggregateId { get; }
    DateTimeOffset OccurredOn { get; }
}