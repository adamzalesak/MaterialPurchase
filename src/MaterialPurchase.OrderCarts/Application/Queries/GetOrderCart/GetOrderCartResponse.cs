namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public record GetOrderCartResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}