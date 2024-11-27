using Carter;
using Microsoft.AspNetCore.Routing;

namespace MaterialPurchase.OrderCarts.Application;

public class OrderCartsEndpoint : CarterModule
{
    // ReSharper disable once MemberCanBeProtected.Global
    public OrderCartsEndpoint() : base("/api/order-carts")
    {
        WithTags("Order Carts");
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app) { }
}