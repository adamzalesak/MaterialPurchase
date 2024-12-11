namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public record OrderProductRequest
{
    public int ProductId { get; init; }
    public Guid OfferId { get; init; }
    public int Quantity { get; init; }
}