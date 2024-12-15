using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferItemAvailableQuantityChangedDomainEvent : DomainEvent, IOfferDomainEvent
{
    public override AggregateType AggregateType => AggregateType.Offer;
    public Guid OfferItemId { get; init; }
    public int Quantity { get; init; }
}