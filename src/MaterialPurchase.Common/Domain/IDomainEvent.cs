namespace MaterialPurchase.Common.Domain;

public interface IDomainEvent
{
    int Version { get; set; }
    AggregateType AggregateType { get; }
    Guid AggregateId { get; }
    DateTimeOffset OccurredOn { get; }
}