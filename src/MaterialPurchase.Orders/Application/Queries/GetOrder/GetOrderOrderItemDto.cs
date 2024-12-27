namespace MaterialPurchase.Orders.Application.Queries.GetOrder;

public record GetOrderOrderItemDto
{
    public Guid OrderItemId { get; init; }
    public int ProductId { get; init; }
    public required string Name { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public required string Currency { get; init; }
}