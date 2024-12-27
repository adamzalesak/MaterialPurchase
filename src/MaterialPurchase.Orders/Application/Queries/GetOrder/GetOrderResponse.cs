namespace MaterialPurchase.Orders.Application.Queries.GetOrder;

public record GetOrderResponse
{
    public Guid OrderId { get; init; }
    public required string Status { get; init; }
    public required IEnumerable<GetOrderOrderItemDto> Items { get; init; }
}