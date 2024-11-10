using Microsoft.CSharp.RuntimeBinder;

namespace MaterialPurchase.Common.Domain;

public abstract class AggregateRoot : Entity<Guid>, IHasDomainEvents
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;


    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);

        try
        {
            ((dynamic)this).Apply((dynamic)domainEvent);
        }
        catch (RuntimeBinderException)
        {
            throw new InvalidOperationException(
                $"Aggregate {GetType().Name} does not handle domain event {domainEvent.GetType().Name}. Implement Apply method.");
        }
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}