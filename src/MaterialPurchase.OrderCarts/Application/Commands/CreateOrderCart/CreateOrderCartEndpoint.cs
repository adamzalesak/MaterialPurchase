using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public class CreateOrderCartEndpoint : OrderCartsEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateOrderCartRequest request, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var id = await bus.InvokeAsync<Guid>(new CreateOrderCartCommand(request.Name), cancellationToken);
                return Results.Ok(id);
            })
            .Produces<Guid>()
            .WithSummary("Create an order cart");
    }
}