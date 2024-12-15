using MaterialPurchase.OrderCartsContracts.ModuleQueries;
using MaterialPurchase.OrderCartsContracts.ModuleQueries.Models;

namespace MaterialPurchase.OrderCarts.Application.ModuleQueryHandlers;

public static class GetOrderCartProductsQueryHandler
{
    public static ICollection<OrderCartItemQueryModel> Handle(GetOrderCartItemsQuery query)
    {
        return new List<OrderCartItemQueryModel>
        {
            new()
            {
                ProductId = 1,
                Price = 45.78m,
                Quantity = 6,
            },
        };
    }
}