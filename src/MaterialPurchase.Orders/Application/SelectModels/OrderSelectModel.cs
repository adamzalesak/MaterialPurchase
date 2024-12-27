using MaterialPurchase.Orders.Domain.Order;

namespace MaterialPurchase.Orders.Application.SelectModels;

public record OrderSelectModel
{
    public Guid OrderId { get; init; }
    public Guid OrderCartId { get; init; }
    public OrderStatus Status { get; init; }
    public int SupplierId { get; init; }
    public required ICollection<OrderItemSelectModel> Items { get; init; }
}