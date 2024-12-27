namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCartStats;

public record GetOrderCartStatsResponse
{
    public int CreatedCount { get; init; }
    public int FinishedCount { get; init; }
}