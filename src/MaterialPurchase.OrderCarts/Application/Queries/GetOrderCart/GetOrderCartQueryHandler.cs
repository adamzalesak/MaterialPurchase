namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public class GetOrderCartQueryHandler
{
    public async Task<GetOrderCartResponse?> Handle(GetOrderCartQuery query, IOrderCartReadRepository readRepository,
        CancellationToken cancellationToken)
    {
        var selectModel = await readRepository.GetOrderCart(query.Id, cancellationToken);

        return selectModel is null
            ? null
            : new GetOrderCartResponse
            {
                Id = selectModel.Id,
                Name = selectModel.Name,
                Status = selectModel.Status.ToString(),
                Items = selectModel.Items
                    .Select(x => new OrderCartItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Quantity = x.Quantity,
                        OfferId = x.OfferId,
                        SupplierId = x.SupplierId,
                        Price = x.Price.Amount,
                        Currency = x.Price.Currency,
                    })
                    .ToList(),
            };
    }
}