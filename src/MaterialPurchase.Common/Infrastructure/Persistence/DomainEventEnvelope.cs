using System.Text.Json;
using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public class DomainEventEnvelope : Entity<Guid>
{
#pragma warning disable CS8618
    public DomainEventEnvelope()
    {
    }
#pragma warning restore CS8618

    public DomainEventEnvelope(IDomainEvent domainEvent)
    {
        AggregateType = domainEvent.AggregateType;
        AggregateId = domainEvent.AggregateId;
        OccurredOn = domainEvent.OccurredOn;
        EventType = domainEvent.GetType().FullName ?? "";
        Data = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
    }

    public AggregateType AggregateType { get; }
    public Guid AggregateId { get; }
    public DateTimeOffset OccurredOn { get; }
    public string Data { get; }
    public string EventType { get; }
}