using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCartStats;

public class GetOrderCartStatsQueryHandler : IQueryHandler<GetOrderCartStatsQuery, GetOrderCartStatsResponse>
{
    private readonly IOrderCartReadRepository _orderCartReadRepository;

    public GetOrderCartStatsQueryHandler(IOrderCartReadRepository orderCartReadRepository)
    {
        _orderCartReadRepository = orderCartReadRepository;
    }

    public async Task<GetOrderCartStatsResponse> Handle(GetOrderCartStatsQuery request, CancellationToken cancellationToken)
    {
        var orderCartStats = await _orderCartReadRepository.GetOrderCartStats(cancellationToken);

        return new GetOrderCartStatsResponse
        {
            CreatedCount = orderCartStats?.CreatedCount ?? 0,
            FinishedCount = orderCartStats?.FinishedCount ?? 0,
        };
    }
}