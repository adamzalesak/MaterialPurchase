namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public record CreateOrderCartRequest
{
    public string Name { get; init; } = string.Empty;
}