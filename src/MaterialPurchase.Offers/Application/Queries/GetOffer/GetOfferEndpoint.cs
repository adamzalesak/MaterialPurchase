using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Offers.Application.Queries.GetOffer;

public class GetOfferEndpoint : OffersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", async ([FromRoute] Guid id, IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var query = new GetOfferQuery(id);
                var response = await bus.InvokeAsync<GetOfferResponse>(query, cancellationToken);

                return Results.Ok(response);
            })
            .Produces<GetOfferResponse>()
            .WithSummary("Get an offer by id");
    }
}