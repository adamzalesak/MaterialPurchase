using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Offers.Application.Commands.ConfirmOffer;

public class ConfirmOfferEndpoint : OffersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/{offerId:guid}/confirm", async ([FromRoute] Guid offerId, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                await bus.InvokeAsync(new ConfirmOfferCommand(offerId), cancellationToken);
                return Results.NoContent();
            })
            .WithSummary("Confirm an offer");
    }
}