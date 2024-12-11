namespace MaterialPurchase.Common.Domain;

public interface IDomainEvent
{
    AggregateType AggregateType { get; }
    Guid AggregateId { get; set; }
    Guid AggregateVersion { get; set; }
    DateTimeOffset OccurredOn { get; }
}