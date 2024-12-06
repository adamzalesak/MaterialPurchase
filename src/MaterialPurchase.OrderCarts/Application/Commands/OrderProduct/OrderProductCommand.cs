namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public record OrderProductCommand
{
    public int ProductId { get; init; }
    public Guid OfferId { get; init; }
    
}