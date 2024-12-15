using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCartsContracts.ModuleQueries;
using MaterialPurchase.OrderCartsContracts.ModuleQueries.Models;
using MaterialPurchase.Orders.Domain.Order;
using MaterialPurchase.Orders.Infrastructure.Persistence;
using Wolverine;

namespace MaterialPurchase.Orders.Application.DomainEventHandlers;

public static class CreateOrderOnOrderCartFinishedDomainEventHandler
{
    public static async Task Handle(OrderCartFinishedDomainEvent @event, IAggregateRepository<Order> repository,
        IOrderReadRepository readRepository, IMessageBus bus, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order cart with id: {@event.AggregateId} finished -> creating order.");

        var alreadyCreated = await readRepository.ExistsByOrderCartId(@event.AggregateId, cancellationToken);
        if (alreadyCreated)
        {
            return;
        }

        // get data from another module by invoking a query
        var orderCartItemsQuery = new GetOrderCartItemsQuery(@event.AggregateId);
        var orderCartItems = await bus.InvokeAsync<ICollection<OrderCartItemQueryModel>>(orderCartItemsQuery, cancellationToken) ??
                             throw new InvalidOperationException("Order cart items not found.");

        _ = orderCartItems;

        var order = Order.Create(orderCartId: @event.AggregateId);

        repository.Add(order);
    }
}