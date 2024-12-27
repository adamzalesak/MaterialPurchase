namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public class GetOrderCartsQueryHandler
{
    public async Task<ICollection<GetOrderCartsResponse>> Handle(GetOrderCartsQuery query, IOrderCartReadRepository readRepository,
        CancellationToken cancellationToken)
    {
        return await readRepository.GetOrderCarts(cancellationToken);
    }
}