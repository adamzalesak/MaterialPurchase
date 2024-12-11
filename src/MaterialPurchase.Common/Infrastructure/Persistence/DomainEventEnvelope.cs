using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public class DomainEventEnvelope : Entity<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SequenceNumber { get; set; }

    public AggregateType AggregateType { get; set; }
    public Guid AggregateId { get; set; }
    public Guid AggregateVersion { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string Data { get; set; }
    public string EventType { get; set; }

    public DomainEventEnvelope(DomainEvent domainEvent)
    {
        Id = Guid.NewGuid();
        AggregateType = domainEvent.AggregateType;
        AggregateId = domainEvent.AggregateId;
        AggregateVersion = domainEvent.AggregateVersion;
        OccurredOn = domainEvent.OccurredOn;
        EventType = domainEvent.GetType().FullName ?? "";
        Data = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
    }

#pragma warning disable CS8618
    public DomainEventEnvelope()
    {
    }
#pragma warning restore CS8618
}