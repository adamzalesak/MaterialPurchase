using MaterialPurchase.Orders.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialPurchase.Orders;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddOrdersModule(this WebApplicationBuilder builder)
    {
        builder.AddInfrastructure();

        return builder;
    }
}