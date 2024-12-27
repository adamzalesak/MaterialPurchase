namespace MaterialPurchase.Common.Domain;

public abstract record DomainEvent
{
    public abstract AggregateType AggregateType { get; }
    public Guid AggregateId { get; set; } = Guid.NewGuid();
    public Guid AggregateVersion { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}