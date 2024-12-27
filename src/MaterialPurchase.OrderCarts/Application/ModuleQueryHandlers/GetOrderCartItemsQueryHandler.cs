using MaterialPurchase.OrderCartsContracts.ModuleQueries;
using MaterialPurchase.OrderCartsContracts.ModuleQueries.Dtos;

namespace MaterialPurchase.OrderCarts.Application.ModuleQueryHandlers;

public static class GetOrderCartItemsQueryHandler
{
    public static async Task<ICollection<OrderCartItemResponse>> Handle(GetOrderCartItemsQuery query,
        IOrderCartReadRepository orderCartReadRepository, CancellationToken cancellationToken)
    {
        var orderCart = await orderCartReadRepository.GetOrderCart(query.OrderCartId, cancellationToken) ??
                        throw new InvalidOperationException($"Order cart with id {query.OrderCartId} not found.");

        return orderCart.Items.Select(x => new OrderCartItemResponse
        {
            ProductId = x.ProductId,
            Price = x.Price,
            Quantity = x.Quantity,
            Name = x.Name,
            SupplierId = x.SupplierId,
        }).ToList();
    }
}