using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Commands.RemoveItem;

public class RemoveOrderCartItemEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:guid}/items/{itemId:guid}",
                async ([FromRoute] Guid id, [FromRoute] Guid itemId, IMessageBus bus, CancellationToken cancellationToken) =>
                {
                    await bus.InvokeAsync(new RemoveOrderCartItemCommand
                    {
                        OrderCartId = id,
                        OrderCartItemId = itemId,
                    }, cancellationToken);
                    return Results.Ok();
                })
            .WithSummary("Remove an item from an order cart");
    }
}