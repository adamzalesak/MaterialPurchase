namespace MaterialPurchase.OrderCartsContracts.IntegrationEvents;

public record OrderCartFinishedIntegrationEvent : IOrderCartIntegrationEvent
{
    public Guid Id { get; init; }
}