using MaterialPurchase.Common.Domain;

namespace MaterialPurchase.OrderCarts.Domain.Entities;

public class OrderCartItem : Entity<Guid>
{
    public Guid OrderCartId { get; private set; }
    public int ProductId { get; private set; }
    public Guid OfferId { get; private set; }
    public int SupplierId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderCartItem(int productId, Guid offerId, int supplierId, int quantity, decimal price)
    {
        ProductId = productId;
        OfferId = offerId;
        SupplierId = supplierId;
        Quantity = quantity;
        Price = price;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}