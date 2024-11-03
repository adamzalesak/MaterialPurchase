using MaterialPurchase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddCommon(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MaterialPurchaseDb")
                               ?? throw new InvalidOperationException("Connection string 'MaterialPurchaseDb' not found.");

        builder.Services.AddDbContext<CommonDbContext>(
            options => { options.UseSqlServer(connectionString); },
            // options must be singleton due to Wolverine integration (this allows to omit the use of DI container in the generated code)
            optionsLifetime: ServiceLifetime.Singleton
        );
        builder.Services.AddMemoryCache();

        return builder;
    }
}