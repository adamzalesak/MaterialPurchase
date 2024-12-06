using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrderCarts.Domain.Entities;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCarts.Domain.Enums;

namespace MaterialPurchase.OrderCarts.Domain;

public class OrderCart(Guid id, string name, OrderCartStatus status) : AggregateRoot(id)
{
    public string Name { get; private set; } = name;
    public OrderCartStatus Status { get; private set; } = status;
    readonly List<OrderCartItem> _orderCartItems = [];
    public IReadOnlyCollection<OrderCartItem> OrderCartItems => _orderCartItems;

    private OrderCart() : this(Guid.Empty, string.Empty, OrderCartStatus.Created)
    {
    }

    public static OrderCart Create(string name)
    {
        var orderCart = new OrderCart();

        var domainEvent = new OrderCartCreatedDomainEvent(Guid.NewGuid(), name);
        orderCart.RaiseDomainEvent(domainEvent);

        return orderCart;
    }

    public void Finish()
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Order cart is already finished");
        }

        var domainEvent = new OrderCartFinishedDomainEvent(Id);
        RaiseDomainEvent(domainEvent);
    }

    public void AddItem(int productId, int quantity, decimal price, int supplierId, Guid offerId)
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Cannot add item to finished order cart");
        }

        var existingItem = _orderCartItems.FirstOrDefault(i =>
            i.ProductId == productId && i.SupplierId == supplierId && i.OfferId == offerId && i.Price == price);
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            var orderCartItem =
                new OrderCartItem(productId: productId, offerId: Guid.NewGuid(), supplierId: 1, quantity: quantity, price: 0);
            _orderCartItems.Add(orderCartItem);
        }
    }

    public void Apply(OrderCartCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.AggregateId;
        Name = domainEvent.Name;
    }

    public void Apply(OrderCartFinishedDomainEvent domainEvent)
    {
        Status = OrderCartStatus.Finished;
    }
}