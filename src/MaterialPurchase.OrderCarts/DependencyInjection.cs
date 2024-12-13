using MaterialPurchase.OrderCarts.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialPurchase.OrderCarts;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddOrderCartsModule(this WebApplicationBuilder builder)
    {
        builder.AddInfrastructure();
        
        return builder;
    }
}