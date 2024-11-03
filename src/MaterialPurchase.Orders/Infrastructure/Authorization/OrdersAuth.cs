using MaterialPurchase.Common.Application.Authorization;
using MaterialPurchase.Common.Application.Authorization.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace MaterialPurchase.Orders.Infrastructure.Authorization;

public static class OrdersAuth
{
    public static void AddAuth(AuthorizationOptions options)
    {
        options.AddPurchasePolicy(OrdersPolicies.CreateOrder, AppRoles.Buyer);
    }
}