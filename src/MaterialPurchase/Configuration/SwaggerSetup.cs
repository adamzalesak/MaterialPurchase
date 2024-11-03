using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MaterialPurchase.Configuration;

public static class SwaggerSetup
{
    public static WebApplicationBuilder SetupSwaggerGen(this WebApplicationBuilder builder)
    {
        var swaggerAuthOptions = builder.Configuration.GetSection(nameof(SwaggerAuthOptions)).Get<SwaggerAuthOptions>();
        if (swaggerAuthOptions is null)
        {
            return builder;
        }

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
            options.ExampleFilters();
            options.SupportNonNullableReferenceTypes();
            
            options.UseAllOfToExtendReferenceSchemas();
            options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
            options.UseAllOfForInheritance();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Azure AD authentication",
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = swaggerAuthOptions.AuthorizationUrl,
                        TokenUrl = swaggerAuthOptions.TokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            { swaggerAuthOptions.Scope, "Purchase Material API access" },
                        },
                    },
                },
                OpenIdConnectUrl = swaggerAuthOptions.OpenIdConnectUrl,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        OpenIdConnectUrl = swaggerAuthOptions.OpenIdConnectUrl,
                    },
                    new[] { swaggerAuthOptions.Scope }
                },
            });
        });

        return builder;
    }

    public static WebApplication UseSwaggerWithUI(this WebApplication app)
    {
        var swaggerAuthOptions = app.Configuration.GetSection(nameof(SwaggerAuthOptions)).Get<SwaggerAuthOptions>();

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
            options.OAuthClientId(swaggerAuthOptions?.ClientId ?? "");
            options.OAuthAppName("Purchase Material API");
            options.OAuthScopeSeparator(" ");
            options.OAuthUsePkce();
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}