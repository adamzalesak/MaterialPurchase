using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;

public record ActiveOfferItemForProductIdDto
{
    public required Guid OfferId { get; init; }
    public required Guid Id { get; init; }
    public required int ProductId { get; init; }
    public int? AvailableQuantity { get; init; }
    public required Money Price { get; init; }
    public required int SupplierId { get; init; }
}