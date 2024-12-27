namespace MaterialPurchase.OrderCarts.Application.Commands.RemoveItem;

public record RemoveOrderCartItemCommand
{
    public Guid OrderCartId { get; init; }
    public Guid OrderCartItemId { get; init; }
}