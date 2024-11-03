using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.OrderCarts.Application.DomainEventHandlers;

public static class OrderCartCreatedDomainEventHandler
{
    public static void Handle(OrderCartCreatedDomainEvent @event)
    {
        Console.WriteLine("[OrderCarts] OrderCartCreatedDomainEventHandler OK");
    }
}