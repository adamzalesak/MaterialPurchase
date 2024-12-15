namespace MaterialPurchase.Offers.Application.Queries.GetOffers;

public record GetOffersResponse
{
    public Guid Id { get; init; }
    public required string Status { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string? Note { get; init; }
}