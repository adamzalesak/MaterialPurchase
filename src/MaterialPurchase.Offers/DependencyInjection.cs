using MaterialPurchase.Offers.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace MaterialPurchase.Offers;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddOffersModule(this WebApplicationBuilder builder)
    {
        builder.AddInfrastructure();
        
        return builder;
    }
}