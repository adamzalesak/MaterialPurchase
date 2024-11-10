using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public class GetOrderCartQueryHandler : IQueryHandler<GetOrderCartQuery, GetOrderCartResponse?>
{
    readonly IOrderCartReadRepository _readRepository;

    public GetOrderCartQueryHandler(IOrderCartReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GetOrderCartResponse?> Handle(GetOrderCartQuery query, CancellationToken cancellationToken)
    {
        var selectModel = await _readRepository.GetOrderCart(query.Id, cancellationToken);

        return selectModel is null ? null : new GetOrderCartResponse { Id = selectModel.Id, Name = selectModel.Name };
    }
}