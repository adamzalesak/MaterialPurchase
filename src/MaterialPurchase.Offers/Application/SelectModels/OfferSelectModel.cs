namespace MaterialPurchase.Offers.Application.SelectModels;

public record OfferSelectModel
{
    public Guid Id { get; init; }
    public required string Status { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string? Note { get; init; }
    public required ICollection<OfferItemSelectModel> Items { get; init; }
}