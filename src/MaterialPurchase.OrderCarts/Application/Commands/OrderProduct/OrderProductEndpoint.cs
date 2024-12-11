using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public class FinishOrderCartEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:guid}/items", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
        {
            await bus.InvokeAsync(new OrderProductCommand
            {
                OrderCartId = id,
            }, cancellationToken);
            return Results.Ok();
        });
    }
}