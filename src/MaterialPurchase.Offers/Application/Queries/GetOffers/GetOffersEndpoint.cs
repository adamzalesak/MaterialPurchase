using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace MaterialPurchase.Offers.Application.Queries.GetOffers;

public class GetOffersEndpoint : OffersEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IMessageBus bus, CancellationToken cancellationToken) =>
            {
                var query = new GetOffersQuery();
                var response = await bus.InvokeAsync<ICollection<GetOffersResponse>>(query, cancellationToken);

                return Results.Ok(response);
            })
            .Produces<ICollection<GetOffersResponse>>()
            .WithSummary("Get all offers");
    }
}