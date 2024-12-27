using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrderCarts.Domain.OrderCart;

public class OrderCartItem : Entity<Guid>
{
    public Guid OrderCartId { get; private set; }
    public int ProductId { get; private set; }
    public string Name { get; private set; }
    public Guid OfferId { get; private set; }
    public int SupplierId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

#pragma warning disable CS8618
    private OrderCartItem()
    {
    }
#pragma warning restore CS8618

    public OrderCartItem(int productId, string name, Guid offerId, int supplierId, int quantity, Money price)
    {
        ProductId = productId;
        Name = name;
        OfferId = offerId;
        SupplierId = supplierId;
        Quantity = quantity;
        Price = price;
    }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}