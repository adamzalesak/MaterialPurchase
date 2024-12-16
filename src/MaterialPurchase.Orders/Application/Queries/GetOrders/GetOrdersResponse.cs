namespace MaterialPurchase.Orders.Application.Queries.GetOrders;

public record GetOrdersResponse
{
    public Guid OrderId { get; init; }
    public Guid OrderCartId { get; init; }
    public required string Status { get; init; }
    public int SupplierId { get; init; }
}