using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Offers.Application.Commands.AddOfferItem;

public class AddOfferItemEndpoint : OffersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/{offerId:guid}/items", async ([FromRoute] Guid offerId, [FromBody] AddOfferItemRequest request, IMessageBus bus,
                CancellationToken cancellationToken) =>
            {
                var command = new AddOfferItemCommand(
                    OfferId: offerId,
                    ProductId: request.ProductId,
                    AvailableQuantity: request.AvailableQuantity,
                    Price: request.Price,
                    CurrencyCode: request.CurrencyCode
                );

                await bus.InvokeAsync(command, cancellationToken);
                return Results.Ok();
            })
            .WithSummary("Add an item to an offer");
    }
}