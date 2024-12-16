using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.Orders.Domain.Order;

public class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; private set; }
    public int ProductId { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

#pragma warning disable CS8618
    private OrderItem()
    {
    }
#pragma warning restore CS8618

    public OrderItem(Guid orderId, int productId, string name, int quantity, Money price)
    {
        OrderId = orderId;
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}