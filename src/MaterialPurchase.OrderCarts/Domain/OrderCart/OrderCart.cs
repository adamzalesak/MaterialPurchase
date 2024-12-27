using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrderCarts.Domain.Dtos;
using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.OrderCarts.Domain.OrderCart;

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

    public void OrderProduct(ProductDto product, OfferItemDto offerItem, int quantity)
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Cannot order product in finished order cart");
        }
        
        var orderCartItem = _items.Find(i =>
            i.ProductId == product.Id &&
            i.OfferId == offerItem.OfferId &&
            i.SupplierId == offerItem.SupplierId &&
            i.Price == offerItem.Price
        );
        
        if (offerItem.AvailableQuantity + (orderCartItem?.Quantity ?? 0) < quantity)
        {
            throw new InvalidOperationException("Not enough quantity available");
        }

        if (orderCartItem is not null && orderCartItem.Quantity != quantity)
        {
            RaiseDomainEvent(new OrderCartItemOrderedQuantityChangedDomainEvent
            {
                OrderCartItemId = orderCartItem.Id,
                Quantity = quantity,
                OriginalQuantity = orderCartItem.Quantity,
                ProductId = product.Id,
                OfferId = offerItem.OfferId,
            });
            return;
        }

        if (orderCartItem is null)
        {
            RaiseDomainEvent(new OrderCartItemOrderedDomainEvent
            {
                OrderCartItemId = Guid.NewGuid(),
                ProductId = product.Id,
                Name = product.Name,
                OfferId = offerItem.OfferId,
                SupplierId = offerItem.SupplierId,
                Quantity = quantity,
                Price = offerItem.Price,
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

        RaiseDomainEvent(new OrderCartItemRemovedDomainEvent
        {
            OrderCartItemId = orderCartItem.Id,
            OriginalQuantity = orderCartItem.Quantity,
            ProductId = orderCartItem.ProductId,
            OfferId = orderCartItem.OfferId,
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

    public void Apply(OrderCartItemOrderedDomainEvent domainEvent)
    {
        var newOrderCartItem = new OrderCartItem(domainEvent.ProductId, domainEvent.Name, domainEvent.OfferId, domainEvent.SupplierId, domainEvent.Quantity,
            domainEvent.Price);
        _items.Add(newOrderCartItem);
    }

    public void Apply(OrderCartItemOrderedQuantityChangedDomainEvent domainEvent)
    {
        var orderCartItem = _items.Find(i => i.Id == domainEvent.OrderCartItemId) ??
                            throw new InvalidOperationException("Order cart item not found");
        orderCartItem.UpdateQuantity(domainEvent.Quantity);
    }
    
    public void Apply(OrderCartItemRemovedDomainEvent domainEvent)
    {
        var orderCartItem = _items.Find(i => i.Id == domainEvent.OrderCartItemId) ??
                            throw new InvalidOperationException("Order cart item not found");
        _items.Remove(orderCartItem);
    }
}