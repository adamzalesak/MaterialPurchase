using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Orders.Application.Queries.GetOrder;

public class GetOrderEndpoint : OrdersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeAsync<GetOrderResponse?>(new GetOrderQuery(id), cancellationToken);

                return result is null ? Results.NotFound() : Results.Ok(result);
            })
            .Produces<GetOrderResponse?>()
            .WithSummary("Get an order by id");
    }
}