using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferConfirmedDomainEvent : DomainEvent, IOfferDomainEvent
{
    public override AggregateType AggregateType => AggregateType.Offer;
    public Guid OfferId { get; init; }
}