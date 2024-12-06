namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public class GetOrderCartsQueryHandler
{
    public async Task<ICollection<GetOrderCartsResponse>> Handle(GetOrderCartsQuery query, IOrderCartReadRepository readRepository,
        CancellationToken cancellationToken)
    {
        var readModel = await readRepository.GetOrderCarts(cancellationToken);

        return readModel
            .Select(x => new GetOrderCartsResponse
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
    }
}