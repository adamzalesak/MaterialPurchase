using Carter;
using MaterialPurchase.Configuration;
using MaterialPurchase.OrderCarts;
using MaterialPurchase.Orders;
using MaterialPurchase.Wolverine;
using Microsoft.AspNetCore.Diagnostics;
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

services.AddControllers();
services.AddCarter();

services.AddEndpointsApiExplorer();

services.AddSwaggerExamplesFromAssemblyOf<Program>();


var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception?.Message == "A task was canceled.")
        {
            await context.Response.WriteAsync("Task cancelled.");
            // How do I ignore this error afterwards?

        }
        else
        {
            // redirect to the error page
            context.Response.Redirect("/Error");
        }
    });
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.UseSwaggerWithUI();

return await app.RunOaktonCommands(args.Length == 0 ? ["run"] : args);