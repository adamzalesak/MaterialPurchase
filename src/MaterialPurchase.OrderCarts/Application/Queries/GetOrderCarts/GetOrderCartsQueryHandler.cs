using MaterialPurchase.Common.Application.CommandsAndQueries;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public class GetOrderCartsQueryHandler : IQueryHandler<GetOrderCartsQuery, ICollection<GetOrderCartsResponse>>
{
    readonly IOrderCartReadRepository _readRepository;
    readonly IProductReadRepository _productReadRepository;

    public GetOrderCartsQueryHandler(IOrderCartReadRepository readRepository, IProductReadRepository productReadRepository)
    {
        _readRepository = readRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<ICollection<GetOrderCartsResponse>> Handle(GetOrderCartsQuery query, CancellationToken cancellationToken)
    {
        var products = await _productReadRepository.GetAllProducts(cancellationToken);
        Console.WriteLine(products.Count);
        
        
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