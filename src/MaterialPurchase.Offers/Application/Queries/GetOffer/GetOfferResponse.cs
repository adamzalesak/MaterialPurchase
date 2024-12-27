namespace MaterialPurchase.Offers.Application.Queries.GetOffer;

public record GetOfferResponse
{
    public Guid Id { get; init; }
    public required string Status { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string? Note { get; init; }
    public required ICollection<OfferItemDto> Items { get; init; }
}