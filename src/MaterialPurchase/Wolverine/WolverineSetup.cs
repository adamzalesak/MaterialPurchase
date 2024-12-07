using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Commands;
using JasperFx.Core;
using MaterialPurchase.Orders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.SqlServer;

namespace MaterialPurchase.Wolverine;

public static class WolverineSetup
{
    private static readonly TimeSpan[] RetryIntervals = [50.Milliseconds(), 250.Milliseconds(), 2.Seconds()];

    public static WebApplicationBuilder SetupWolverine(this WebApplicationBuilder builder)
    {
        builder.Host.UseWolverine(opts =>
        {
            opts.ApplicationAssembly = typeof(Program).Assembly;
            opts.Discovery.IncludeAssembly(typeof(DependencyInjection).Assembly);
            opts.Discovery.IncludeAssembly(typeof(OrderCarts.DependencyInjection).Assembly);


            opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
            if (builder.Environment.IsDevelopment())
            {
                opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Dynamic;
                opts.Durability.Mode = DurabilityMode.Solo;
            }

            opts.Services.AssertAllExpectedPreBuiltTypesExistOnStartUp();


            var connectionString = builder.Configuration.GetConnectionString("MaterialPurchaseDb")
                                   ?? throw new InvalidOperationException("Connection string 'MaterialPurchaseDb' not found.");
            opts.PersistMessagesWithSqlServer(connectionString, "wolverine");

            opts.Policies.UseDurableLocalQueues();
            opts.Policies.UseDurableInboxOnAllListeners();
            opts.Policies.UseDurableOutboxOnAllSendingEndpoints();


            opts.UseFluentValidation();
            opts.Policies.AddMiddleware(typeof(TransactionScopeMiddleware));


            opts.OnException<SqlException>().RetryWithCooldown(RetryIntervals);
            opts.OnException<DbUpdateException>().RetryWithCooldown(RetryIntervals);
            opts.OnException<TimeoutException>().RetryWithCooldown(RetryIntervals);

            opts.OnException<DbUpdateConcurrencyException>().RetryWithCooldown(RetryIntervals);


            opts.SetupLocalQueues();
            opts.SetupKafka(builder);
        });

        return builder;
    }
}