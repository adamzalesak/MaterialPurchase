using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrderCarts.Application.SelectModels;

public record OrderCartItemSelectModel
{
    public Guid Id { get; init; }
    public int ProductId { get; init; }
    public required string Name { get; init; }
    public Guid OfferId { get; init; }
    public int SupplierId { get; init; }
    public int Quantity { get; init; }
    public required Money Price { get; init; }
}