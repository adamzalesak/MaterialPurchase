namespace MaterialPurchase.Common.Domain;

public abstract class AggregateRoot : Entity<Guid>, IHasDomainEvents
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;


    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}