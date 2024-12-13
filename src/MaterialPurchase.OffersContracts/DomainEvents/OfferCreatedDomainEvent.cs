using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferCreatedDomainEvent : DomainEvent
{
    public override AggregateType AggregateType => AggregateType.Offer;
}