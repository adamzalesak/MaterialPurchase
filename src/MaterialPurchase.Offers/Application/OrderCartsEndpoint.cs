using Carter;
using Microsoft.AspNetCore.Routing;

namespace MaterialPurchase.Offers.Application;

public class OffersEndpoint : CarterModule
{
    // ReSharper disable once MemberCanBeProtected.Global
    public OffersEndpoint() : base("/api/offers")
    {
        WithTags("Offers");
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app) { }
}