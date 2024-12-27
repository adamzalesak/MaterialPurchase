using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialPurchase.Common.Domain;

public abstract class AggregateRoot : Entity<Guid>
{
    [ConcurrencyCheck] public Guid Version { get; set; }

    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    readonly List<DomainEvent> _domainEvents = [];
    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;
    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        ((dynamic)this).Apply((dynamic)domainEvent);

        Version = domainEvent.AggregateVersion;

        if (Id == Guid.Empty)
        {
            throw new InvalidOperationException("Aggregate Id is not set");
        }

        domainEvent.AggregateId = Id;

        _domainEvents.Add(domainEvent);
    }

    // this Apply method is called when no specific Apply method is found in the inheriting class
    public void Apply(DomainEvent domainEvent)
    {
        Console.WriteLine($"Aggregate {GetType().Name} does not handle domain event {domainEvent.GetType().Name}");
    }
}