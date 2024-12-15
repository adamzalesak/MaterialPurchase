using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.OrderCart;

namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public static class CreateOrderCartCommandHandler
{
    public static Guid Handle(CreateOrderCartCommand command, IAggregateRepository<OrderCart> repository)
    {
        var orderCart = OrderCart.Create(command.Name);

        repository.Add(orderCart);

        /*
        // you can publish messages like this (but this message is already created in the domain
        // and will be published automatically by DbContextBase SaveChangesAsync override)
        var @event = new OrderCartCreatedDomainEvent(orderCart.Id);
        await bus.PublishAsync(@event, new DeliveryOptions { PartitionKey = orderCart.Id.ToString() });
        */

        return orderCart.Id;
    }
}