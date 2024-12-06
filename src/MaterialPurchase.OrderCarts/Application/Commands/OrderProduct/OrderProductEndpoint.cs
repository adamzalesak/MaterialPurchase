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
        app.MapPut("/{id:guid}/order", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
        {
            await bus.InvokeAsync(new OrderProductCommand(), cancellationToken);
            return Results.Ok();
        });
    }
}