using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialPurchase.OrderCarts.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MaterialPurchaseDb")
                               ?? throw new InvalidOperationException("Connection string 'MaterialPurchaseDb' not found.");


        builder.Services.AddDbContext<OrderCartsDbContext>(
            options => { options.UseSqlServer(connectionString); },
            // options must be singleton due to Wolverine integration (this allows to omit the use of DI container in the generated code)
            optionsLifetime: ServiceLifetime.Singleton
        );

        builder.Services.AddScoped<IAggregateRepository<OrderCart>, EfAggregateRepository<OrderCart, OrderCartsDbContext>>();

        builder.Services.AddScoped<IOrderCartReadRepository, OrderCartReadRepository>();
        builder.Services.AddScoped<IOrderCartReadModelRepository, OrderCartReadModelRepository>();
        builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();

        return builder;
    }
}