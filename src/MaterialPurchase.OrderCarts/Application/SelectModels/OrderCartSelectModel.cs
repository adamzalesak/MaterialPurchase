using MaterialPurchase.OrderCarts.Domain.OrderCart;

namespace MaterialPurchase.OrderCarts.Application.SelectModels;

public record OrderCartSelectModel
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required OrderCartStatus Status { get; init; }
    public required ICollection<OrderCartItemSelectModel> Items { get; init; }
}