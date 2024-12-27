namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public record GetOrderCartsResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
}