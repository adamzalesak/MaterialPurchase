using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.Orders.Application.DomainEventHandlers;

public static class OrderCartCreatedDomainEventHandler
{
    public static void Handle(OrderCartCreatedDomainEvent @event)
    {
        Console.WriteLine("[Orders] OrderCartCreatedDomainEventHandler OK");
    }
}