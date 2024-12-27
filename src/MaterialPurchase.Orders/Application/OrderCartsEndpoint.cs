using Carter;
using Microsoft.AspNetCore.Routing;

namespace MaterialPurchase.Orders.Application;

public class OrdersEndpoint : CarterModule
{
    // ReSharper disable once MemberCanBeProtected.Global
    public OrdersEndpoint() : base("/api/orders")
    {
        WithTags("Orders");
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app) { }
}