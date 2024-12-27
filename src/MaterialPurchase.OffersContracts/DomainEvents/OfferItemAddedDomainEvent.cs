using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferItemAddedDomainEvent : DomainEvent, IOfferDomainEvent
{
    public override AggregateType AggregateType => AggregateType.Offer;
    public Guid OfferId { get; init; }
    public Guid OfferItemId { get; init; }
    public int ProductId { get; init; }
    public int? AvailableQuantity { get; init; }
    public required Money Price { get; init; }
}