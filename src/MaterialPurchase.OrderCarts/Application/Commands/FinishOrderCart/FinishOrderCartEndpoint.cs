using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;

public class FinishOrderCartEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:guid}/finish", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                await bus.InvokeAsync(new FinishOrderCartCommand(id), cancellationToken);
                return Results.Ok();
            })
            .WithSummary("Finish an order cart");
    }
}