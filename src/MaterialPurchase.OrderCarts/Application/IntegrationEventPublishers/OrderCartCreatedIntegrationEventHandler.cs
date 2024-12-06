using MaterialPurchase.OrderCartsContracts.IntegrationEvents;

namespace MaterialPurchase.OrderCarts.Application.IntegrationEventPublishers;

public class OrderCartCreatedIntegrationEventHandler
{
    public void Handle(OrderCartCreatedIntegrationEvent @event)
    {
        Console.WriteLine("tralalá");
    }
}