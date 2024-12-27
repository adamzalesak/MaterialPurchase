using MaterialPurchase.Common.Domain;
using MaterialPurchase.Orders.Domain.Order.Dtos;
using MaterialPurchase.OrdersContracts.DomainEvents;
using MaterialPurchase.OrdersContracts.DomainEvents.Dtos;

namespace MaterialPurchase.Orders.Domain.Order;

public class Order : AggregateRoot
{
    public Guid OrderCartId { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.New;
    public int SupplierId { get; private set; }
    List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
    }

    public static Order Create(Guid orderCartId, int supplierId, ICollection<OrderItemDto> items)
    {
        var order = new Order();

        order.RaiseDomainEvent(new OrderCreatedDomainEvent
        {
            AggregateId = Guid.NewGuid(),
            OrderCartId = orderCartId,
            SupplierId = supplierId,
            Items = items.Select(x => new OrderCreatedOrderItemDto
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price,
            }).ToList(),
        });

        return order;
    }

    public void Apply(OrderCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.AggregateId;
        OrderCartId = domainEvent.OrderCartId;
        SupplierId = domainEvent.SupplierId;
        _items = domainEvent.Items.Select(x =>
            new OrderItem(x.OrderId, x.ProductId, x.Name, x.Quantity, x.Price)
        ).ToList();
    }
}