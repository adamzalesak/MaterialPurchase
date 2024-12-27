namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public record OrderCartItemDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public Guid OfferId { get; init; }
    public int SupplierId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public required string Currency { get; init; }
}