using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrderCarts.Domain.Dtos;

public record OfferItemDto
{
    public Guid OfferId { get; init; }
    public Guid Id { get; init; }
    public int ProductId { get; init; }
    public int? AvailableQuantity { get; init; }
    public required Money Price { get; init; }
    public int SupplierId { get; init; }
}