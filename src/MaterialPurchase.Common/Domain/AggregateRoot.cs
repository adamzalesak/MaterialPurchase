using System.ComponentModel.DataAnnotations;

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

    readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;


    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        ((dynamic)this).Apply((dynamic)domainEvent);

        Version = Guid.NewGuid();

        domainEvent.AggregateVersion = Version;
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    // Apply method is called when no specific Apply method is found in the derived class
    public void Apply(IDomainEvent domainEvent)
    {
        Console.WriteLine($"Aggregate {GetType().Name} does not handle domain event {domainEvent.GetType().Name}");
    }
}