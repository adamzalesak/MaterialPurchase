using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrderCarts.Domain.Dtos;
using MaterialPurchase.OrderCarts.Domain.Entities;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCarts.Domain.Enums;

namespace MaterialPurchase.OrderCarts.Domain;

public class OrderCart(Guid id, string name, OrderCartStatus status) : AggregateRoot(id)
{
    public string Name { get; private set; } = name;
    public OrderCartStatus Status { get; private set; } = status;
    readonly List<OrderCartItem> _items = [];
    public IReadOnlyCollection<OrderCartItem> Items => _items;

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

        var domainEvent = new OrderCartFinishedDomainEvent();
        RaiseDomainEvent(domainEvent);
    }

    public void OrderProduct(ProductDto product, Guid offerId, int supplierId, int quantity, decimal price)
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Cannot order product in finished order cart");
        }

        var orderCartItem = _items.Find(i =>
            i.ProductId == product.Id &&
            i.OfferId == offerId &&
            i.SupplierId == supplierId &&
            i.Price == price
        );

        if (orderCartItem is not null && orderCartItem.Quantity != quantity)
        {
            RaiseDomainEvent(new OrderCartItemOrderedQuantityChanged
            {
                OrderCartItemId = orderCartItem.Id,
                Quantity = quantity,
            });
            return;
        }

        if (orderCartItem is null)
        {
            RaiseDomainEvent(new OrderCartItemOrdered
            {
                OrderCartItemId = Guid.NewGuid(),
                ProductId = product.Id,
                Name = product.Name,
                OfferId = offerId,
                SupplierId = supplierId,
                Quantity = quantity,
                Price = price,
            });
        }
    }
    
    public void RemoveItem(Guid orderCartItemId)
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Cannot remove product from finished order cart");
        }

        var orderCartItem = _items.Find(i => i.Id == orderCartItemId) ??
                            throw new InvalidOperationException("Order cart item not found");

        RaiseDomainEvent(new OrderCartItemRemoved
        {
            OrderCartItemId = orderCartItem.Id,
        });
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

    public void Apply(OrderCartItemOrdered domainEvent)
    {
        var newOrderCartItem = new OrderCartItem(domainEvent.ProductId, domainEvent.Name, domainEvent.OfferId, domainEvent.SupplierId, domainEvent.Quantity,
            domainEvent.Price);
        _items.Add(newOrderCartItem);
    }

    public void Apply(OrderCartItemOrderedQuantityChanged domainEvent)
    {
        var orderCartItem = _items.Find(i => i.Id == domainEvent.OrderCartItemId) ??
                            throw new InvalidOperationException("Order cart item not found");
        orderCartItem.UpdateQuantity(domainEvent.Quantity);
    }
    
    public void Apply(OrderCartItemRemoved domainEvent)
    {
        var orderCartItem = _items.Find(i => i.Id == domainEvent.OrderCartItemId) ??
                            throw new InvalidOperationException("Order cart item not found");
        _items.Remove(orderCartItem);
    }
}