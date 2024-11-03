using MaterialPurchase.Common.Application.Authorization;
using MaterialPurchase.Handlers;
using MaterialPurchase.OrderCarts.Infrastructure.Authorization;
using MaterialPurchase.Orders.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace MaterialPurchase.Configuration;

public static class AuthConfigurationExtension
{
    internal static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, ExistingDbUserAuthorizationHandler>();
        
        services.AddAuthorization(options =>
        {
            OrderCartsAuth.AddAuth(options);
            OrdersAuth.AddAuth(options);
            
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddRequirements(new ExistingDbUserRequirement())
                .Build();
        });

        return services;
    }
}