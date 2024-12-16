using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Offers.Application.Commands.CreateOffer;

public class CreateOfferEndpoint : OffersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateOfferRequest request, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var id = await bus.InvokeAsync<Guid>(
                    new CreateOfferCommand(request.SupplierId, request.ValidFrom, request.ValidTo, request.Note), cancellationToken);
                return Results.Ok(id);
            })
            .Produces<Guid>()
            .WithSummary("Create an offer");
    }
}