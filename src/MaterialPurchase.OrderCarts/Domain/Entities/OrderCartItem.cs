using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCarts.Domain.Entities;

public class OrderCartItem(
    int id,
    int orderCartId,
    int productId,
    int quantity,
    decimal price
) : Entity<int>(id)
{
    public int OrderCartId { get; set; } = orderCartId;
    public int ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;
}