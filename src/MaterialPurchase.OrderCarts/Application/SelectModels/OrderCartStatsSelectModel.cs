namespace MaterialPurchase.OrderCarts.Application.SelectModels;

public record OrderCartStatsSelectModel
{
    public int CreatedCount { get; init; }
    public int FinishedCount { get; init; }
}