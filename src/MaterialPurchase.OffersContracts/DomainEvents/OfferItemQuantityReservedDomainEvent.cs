using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferItemQuantityReservedDomainEvent : DomainEvent 
{
    public override AggregateType AggregateType => AggregateType.Offer;
    public Guid OfferId { get; init; }
    public Guid OfferItemId { get; init; }
    public int Quantity { get; init; }
}