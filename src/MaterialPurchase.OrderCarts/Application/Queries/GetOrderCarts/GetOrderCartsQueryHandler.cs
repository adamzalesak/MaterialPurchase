using MaterialPurchase.Common.Application.CommandsAndQueries;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public class GetOrderCartsQueryHandler : IQueryHandler<GetOrderCartsQuery, ICollection<GetOrderCartsResponse>>
{
    private readonly IOrderCartReadRepository _readRepository;

    public GetOrderCartsQueryHandler(IOrderCartReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<ICollection<GetOrderCartsResponse>> Handle(GetOrderCartsQuery query, CancellationToken cancellationToken)
    {
        var readModel = await _readRepository.GetOrderCarts(cancellationToken);

        return readModel
            .Select(x => new GetOrderCartsResponse
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
    }
}