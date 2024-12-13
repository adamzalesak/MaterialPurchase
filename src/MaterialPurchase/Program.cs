using Carter;
using MaterialPurchase.Configuration;
using MaterialPurchase.Offers;
using MaterialPurchase.OrderCarts;
using MaterialPurchase.Orders;
using MaterialPurchase.Wolverine;
using Oakton;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
    });

builder
    .AddOrderCartsModule()
    .AddOrdersModule()
    .AddOffersModule()
    ;

builder.SetupWolverine();
builder.Host.ApplyOaktonExtensions();

builder.SetupSwaggerGen();

var services = builder.Services;

services.AddHealthChecks();

services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            ;
    });
});

services.AddCarter();
services.AddEndpointsApiExplorer();
services.AddSwaggerExamplesFromAssemblyOf<Program>();


var app = builder.Build();

app.MapCarter();
app.UseCors();
app.UseSwaggerWithUI();

return await app.RunOaktonCommands(args.Length == 0 ? ["run"] : args);