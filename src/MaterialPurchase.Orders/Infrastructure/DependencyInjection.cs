using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Orders.Application;
using MaterialPurchase.Orders.Domain.Order;
using MaterialPurchase.Orders.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialPurchase.Orders.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MaterialPurchaseDb")
                               ?? throw new InvalidOperationException("Connection string 'MaterialPurchaseDb' not found.");

        builder.Services.AddDbContext<OrdersDbContext>(
            options => { options.UseSqlServer(connectionString); },
            optionsLifetime: ServiceLifetime.Singleton
        );

        builder.Services.AddScoped<IAggregateRepository<Order>, EfAggregateRepository<Order, OrdersDbContext>>();
        
        builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

        return builder;
    }
}