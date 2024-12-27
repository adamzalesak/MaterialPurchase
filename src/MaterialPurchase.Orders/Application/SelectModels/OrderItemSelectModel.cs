using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.Orders.Application.SelectModels;

public record OrderItemSelectModel
{
    public Guid OrderItemId { get; init; }
    public Guid OrderId { get; init; }
    public int ProductId { get; init; }
    public required string Name { get; init; }
    public int Quantity { get; init; }
    public required Money Price { get; init; }
}