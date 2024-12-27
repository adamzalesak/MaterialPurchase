using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MaterialPurchase.Configuration;

public static class SwaggerSetup
{
    public static WebApplicationBuilder SetupSwaggerGen(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
            options.ExampleFilters();
            options.SupportNonNullableReferenceTypes();
            
            options.UseAllOfToExtendReferenceSchemas();
            options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
            options.UseAllOfForInheritance();
        });

        return builder;
    }

    public static WebApplication UseSwaggerWithUI(this WebApplication app)
    {

        app.UseSwagger(options =>
        {
            options.RouteTemplate = ".less-known/api-docs/{documentName}.json";
            options.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                // Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
                swagger.Servers.Clear();
            });
        });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/.less-known/api-docs/v1.json", "Purchase Material API v1");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}