using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Commands;
using JasperFx.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.SqlServer;

namespace MaterialPurchase.Wolverine;

public static class WolverineSetup
{
    static readonly TimeSpan[] DefaultRetryIntervals = [50.Milliseconds(), 250.Milliseconds(), 2.Seconds()];

    public static WebApplicationBuilder SetupWolverine(this WebApplicationBuilder builder)
    {
        builder.Host.UseWolverine(opts =>
        {
            opts.ApplicationAssembly = typeof(Program).Assembly;
            opts.Discovery.IncludeAssembly(typeof(Orders.DependencyInjection).Assembly);
            opts.Discovery.IncludeAssembly(typeof(OrderCarts.DependencyInjection).Assembly);
            opts.Discovery.IncludeAssembly(typeof(Offers.DependencyInjection).Assembly);

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
            opts.Policies.AddMiddleware<TransactionalMiddleware>(chain => !chain.MessageType.Name.EndsWith("Query"));


            opts.OnException<SqlException>().RetryWithCooldown(DefaultRetryIntervals)
                .Then.ScheduleRetryIndefinitely(5.Minutes());
            opts.OnException<DbUpdateException>().RetryWithCooldown(DefaultRetryIntervals)
                .Then.ScheduleRetryIndefinitely(5.Minutes());
            opts.OnException<TimeoutException>().RetryWithCooldown(DefaultRetryIntervals)
                .Then.ScheduleRetryIndefinitely(5.Minutes());

            opts.OnException<DbUpdateConcurrencyException>().RetryWithCooldown(DefaultRetryIntervals)
                .Then.ScheduleRetryIndefinitely(5.Minutes());


            opts.SetupLocalQueues();
            opts.SetupKafka(builder);
        });

        return builder;
    }
}