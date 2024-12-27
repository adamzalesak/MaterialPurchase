using Azure.Core;
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
        app.MapPost("/{id:guid}/items",
                async ([FromRoute] Guid id, [FromBody] OrderProductRequest request, IMessageBus bus, CancellationToken cancellationToken) =>
                {
                    await bus.InvokeAsync(new OrderProductCommand
                    {
                        OrderCartId = id,
                        ProductId = request.ProductId,
                        OfferId = request.OfferId,
                        Quantity = request.Quantity,
                    }, cancellationToken);
                    return Results.Ok();
                })
            .WithSummary("Order a product");
    }
}