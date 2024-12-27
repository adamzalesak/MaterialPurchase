using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OffersContracts.DomainEvents;

public record OfferCreatedDomainEvent : DomainEvent, IOfferDomainEvent
{
    public override AggregateType AggregateType => AggregateType.Offer;
    public int SupplierId { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string? Note { get; init; }
}