namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public record OrderProductCommand
{
    public Guid OrderCartId { get; init; }
    public int ProductId { get; init; }
    public Guid OfferId { get; init; }
    public int Quantity { get; init; }
}