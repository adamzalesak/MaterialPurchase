namespace MaterialPurchase.Offers.Application.SelectModels;

public record OfferItemSelectModel
{
    public Guid OfferId { get; init; }
    public Guid Id { get; init; }
    public int ProductId { get; init; }
    public int? AvailableQuantity { get; init; }
    public decimal Price { get; init; }
    public required string Currency { get; init; }
}