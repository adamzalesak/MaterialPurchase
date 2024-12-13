namespace MaterialPurchase.Offers.Application.Commands.CreateOffer;

public record CreateOfferCommand(
    int SupplierId,
    DateTimeOffset ValidFrom,
    DateTimeOffset? ValidTo,
    string? Note
);