using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrdersContracts.DomainEvents;

namespace MaterialPurchase.Orders.Domain.Order;

public class Order : AggregateRoot
{
    public Guid OrderCartId { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.New;

    private Order()
    {
    }
    
    public static Order Create(Guid orderCartId)
    {
        var order = new Order
        {
            OrderCartId = orderCartId,
        };

        var domainEvent = new OrderCreatedDomainEvent(order.Id);
        order.RaiseDomainEvent(domainEvent);

        return order;
    }
}