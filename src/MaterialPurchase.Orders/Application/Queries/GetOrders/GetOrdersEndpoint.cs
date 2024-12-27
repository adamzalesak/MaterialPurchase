using MaterialPurchase.Orders.Application.Queries.GetOrder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Orders.Application.Queries.GetOrders;

public class GetOrdersEndpoint : OrdersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeAsync<ICollection<GetOrdersResponse>>(new GetOrders.GetOrdersQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .Produces<GetOrderResponse?>()
            .WithSummary("Get all orders");
    }
}