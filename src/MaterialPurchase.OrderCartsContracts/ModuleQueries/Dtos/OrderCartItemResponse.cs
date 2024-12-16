using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrderCartsContracts.ModuleQueries.Dtos;

public class OrderCartItemResponse
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public required Money Price { get; init; }
    public required string Name { get; init; }
    public int SupplierId { get; init; }
}