using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCartsContracts.ModuleQueries;
using MaterialPurchase.OrderCartsContracts.ModuleQueries.Dtos;
using MaterialPurchase.Orders.Domain.Order;
using MaterialPurchase.Orders.Domain.Order.Dtos;
using MaterialPurchase.Orders.Infrastructure.Persistence;
using Wolverine;

namespace MaterialPurchase.Orders.Application.DomainEventHandlers;

public static class CreateOrdersOnOrderCartFinishedDomainEventHandler
{
    public static async Task Handle(OrderCartFinishedDomainEvent @event, IAggregateRepository<Order> repository,
        IOrderReadRepository readRepository, IMessageBus bus, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order cart with id {@event.AggregateId} finished -> creating orders.");

        var alreadyCreated = await readRepository.ExistsByOrderCartId(@event.AggregateId, cancellationToken);
        if (alreadyCreated)
        {
            return;
        }

        var orderCartItemsQuery = new GetOrderCartItemsQuery(@event.AggregateId);
        var orderCartItems = await bus.InvokeAsync<ICollection<OrderCartItemResponse>>(orderCartItemsQuery, cancellationToken) ??
                             throw new InvalidOperationException("Order cart items not found.");

        var itemsBySupplier = orderCartItems.GroupBy(x => x.SupplierId).ToList();

        foreach (var supplierItems in itemsBySupplier)
        {
            var orderItems = supplierItems.Select(x => new OrderItemDto
            {
                OrderId = @event.AggregateId,
                ProductId = x.ProductId,
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price,
            }).ToList();

            var order = Order.Create(orderCartId: @event.AggregateId, supplierId: supplierItems.Key, items: orderItems);

            repository.Add(order);
        }
    }
}