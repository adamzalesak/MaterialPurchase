namespace MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;

public record GetActiveOfferItemsForProductIdResponse
{
    public required ICollection<ActiveOfferItemForProductIdDto> OfferItems { get; init; }
}