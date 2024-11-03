namespace MaterialPurchase.Common.Domain;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}