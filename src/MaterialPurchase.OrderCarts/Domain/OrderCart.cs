using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCarts.Domain.Enums;

namespace MaterialPurchase.OrderCarts.Domain;

public class OrderCart(Guid id, string name, OrderCartStatus status) : AggregateRoot(id)
{
    public string Name { get; private set; } = name;
    public OrderCartStatus Status { get; private set; } = status;
    
    private OrderCart() : this(Guid.Empty, string.Empty, OrderCartStatus.Created)
    {
    }

    public static OrderCart Create(string name)
    {
        var orderCart = new OrderCart(Guid.NewGuid(), name, OrderCartStatus.Created);

        var domainEvent = new OrderCartCreatedDomainEvent(orderCart.Id);
        orderCart.RaiseDomainEvent(domainEvent);

        return orderCart;
    }

    public void Finish()
    {
        if (Status == OrderCartStatus.Finished)
        {
            throw new InvalidOperationException("Order cart is already finished");
        }

        Status = OrderCartStatus.Finished;

        var domainEvent = new OrderCartFinishedDomainEvent(Id);
        RaiseDomainEvent(domainEvent);
    }
}