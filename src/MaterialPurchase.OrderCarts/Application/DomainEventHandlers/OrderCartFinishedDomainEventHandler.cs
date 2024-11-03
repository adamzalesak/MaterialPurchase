using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application.DomainEventHandlers;

public static class OrderCartFinishedDomainEventHandler
{
    public static void Handle(OrderCartFinishedDomainEvent @event, IAggregateRepository<OrderCart> repository, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            "Second handler for OrderCartFinishedDomainEvent. If this handler raises an exception, the transaction should be rolled back.");

        /*
        // publish message - the message should not be saved to the outbox if the transaction is rolled back
        // also, the message should not be flushed (handled) if the transaction is rolled back
        var createdDomainEvent = new OrderCartCreatedDomainEvent(Guid.NewGuid());
        await bus.PublishAsync(createdDomainEvent);

        // create new order cart - the order cart should not be saved to the database if the transaction is rolled back
        var newOc = OrderCart.Create("New Order Cart");
        repository.Create(newOc);
        await repository.SaveChangesAsync(cancellationToken);

        throw new ArithmeticException();
        */
    }
}