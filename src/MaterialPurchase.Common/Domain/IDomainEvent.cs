namespace MaterialPurchase.Common.Domain;

public interface IDomainEvent
{
    AggregateType AggregateType { get; }
    Guid AggregateId { get; }
    Guid AggregateVersion { get; set; }
    DateTimeOffset OccurredOn { get; }
}