namespace MaterialPurchase.Offers.Application.Commands.AddOfferItem;

public record AddOfferItemCommand(
    Guid OfferId,
    int ProductId,
    int? AvailableQuantity,
    decimal Price,
    string CurrencyCode
);