using MaterialPurchase.Common.Application.Authorization;
using MaterialPurchase.Common.Application.Authorization.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace MaterialPurchase.OrderCarts.Infrastructure.Authorization;

public static class OrderCartsAuth
{
    public static void AddAuth(AuthorizationOptions options)
    {
        options.AddPurchasePolicy(OrderCartsPolicies.GetOrderCarts, AppRoles.Buyer);
    }
}