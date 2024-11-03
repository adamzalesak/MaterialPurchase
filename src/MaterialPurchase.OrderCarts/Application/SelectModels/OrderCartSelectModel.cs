namespace MaterialPurchase.OrderCarts.Application.SelectModels;

public record OrderCartSelectModel
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}