namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCartStats;

public class GetOrderCartStatsQueryHandler
{
    public async Task<GetOrderCartStatsResponse> Handle(GetOrderCartStatsQuery request, IOrderCartReadRepository orderCartReadRepository,
        CancellationToken cancellationToken)
    {
        var orderCartStats = await orderCartReadRepository.GetOrderCartStats(cancellationToken);

        return new GetOrderCartStatsResponse
        {
            CreatedCount = orderCartStats?.CreatedCount ?? 0,
            FinishedCount = orderCartStats?.FinishedCount ?? 0,
        };
    }
}