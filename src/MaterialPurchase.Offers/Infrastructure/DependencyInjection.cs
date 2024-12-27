using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Application;
using MaterialPurchase.Offers.Domain;
using MaterialPurchase.Offers.Domain.Offer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialPurchase.Offers.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MaterialPurchaseDb")
                               ?? throw new InvalidOperationException("Connection string 'MaterialPurchaseDb' not found.");

        builder.Services.AddDbContext<OffersDbContext>(
            options => { options.UseSqlServer(connectionString); },
            // options must be singleton due to Wolverine integration (this allows to omit the use of DI container in the generated code)
            optionsLifetime: ServiceLifetime.Singleton
        );

        builder.Services.AddScoped<IAggregateRepository<Offer>, EfAggregateRepository<Offer, OffersDbContext>>();

        builder.Services.AddScoped<IOfferReadRepository, OfferReadRepository>();
        builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();

        return builder;
    }
}