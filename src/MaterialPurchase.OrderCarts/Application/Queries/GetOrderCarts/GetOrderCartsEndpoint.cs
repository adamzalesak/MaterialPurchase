using MaterialPurchase.Common.Application.CommandsAndQueries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public class GetOrderCartsEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeQueryAsync(new GetOrderCartsQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .Produces<ICollection<GetOrderCartsResponse>>();
    }
}