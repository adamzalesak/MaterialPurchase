using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public class GetOrderCartEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeAsync<GetOrderCartResponse?>(new GetOrderCartQuery(id), cancellationToken);

                return result is null ? Results.NotFound() : Results.Ok(result);
            })
            .Produces<GetOrderCartResponse?>()
            .WithSummary("Get an order cart by id");
    }
}