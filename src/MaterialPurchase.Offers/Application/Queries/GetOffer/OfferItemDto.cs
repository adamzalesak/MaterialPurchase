namespace MaterialPurchase.Offers.Application.Queries.GetOffer;

public record OfferItemDto
{
    public Guid OfferId { get; init; }
    public Guid Id { get; init; }
    public int ProductId { get; init; }
    public required string ProductName { get; init; }
    public required string ProductCode { get; init; }
    public int? AvailableQuantity { get; init; }
    public decimal Price { get; init; }
    public required string Currency { get; init; }
}