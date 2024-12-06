namespace MaterialPurchase.OrderCartsContracts.IntegrationEvents;

public record OrderCartCreatedIntegrationEvent
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}