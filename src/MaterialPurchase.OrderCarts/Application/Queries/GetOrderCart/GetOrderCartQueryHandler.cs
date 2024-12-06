namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public class GetOrderCartQueryHandler
{
    public async Task<GetOrderCartResponse?> Handle(GetOrderCartQuery query, IOrderCartReadRepository readRepository, CancellationToken cancellationToken)
    {
        var selectModel = await readRepository.GetOrderCart(query.Id, cancellationToken);

        return selectModel is null ? null : new GetOrderCartResponse { Id = selectModel.Id, Name = selectModel.Name };
    }
}