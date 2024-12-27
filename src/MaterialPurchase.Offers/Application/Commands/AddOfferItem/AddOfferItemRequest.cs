namespace MaterialPurchase.Offers.Application.Commands.AddOfferItem;

public record AddOfferItemRequest
{
    public int ProductId { get; init; }
    public int? AvailableQuantity { get; init; }
    public decimal Price { get; init; }
    public required string CurrencyCode { get; init; }
}