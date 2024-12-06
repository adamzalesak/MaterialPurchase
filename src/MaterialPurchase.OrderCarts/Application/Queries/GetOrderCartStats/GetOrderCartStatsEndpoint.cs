using MaterialPurchase.Common.Application.CommandsAndQueries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCartStats;

public class GetOrderCartStatsEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/stats", async (IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeQueryAsync(new GetOrderCartStatsQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .Produces<ICollection<GetOrderCartStatsQuery>>();
    }
}