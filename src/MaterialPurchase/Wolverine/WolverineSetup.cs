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

            opts.OnException<SqlException>().RetryWithCooldown(50.Milliseconds(), 250.Milliseconds(), 2.Seconds());
            opts.OnException<DbUpdateException>().RetryWithCooldown(50.Milliseconds(), 250.Milliseconds(), 2.Seconds());
            opts.OnException<TimeoutException>().RetryWithCooldown(50.Milliseconds(), 250.Milliseconds(), 2.Seconds());

            // opts.UseKafka("business-support-franz-bootstrap.k8s.notino.dev:32500")
            //     .ConfigureClient(c =>
            //     {
            //         c.SaslUsername = "";
            //         c.SaslPassword = "";
            //         c.SaslMechanism =  SaslMechanism.ScramSha512;
            //         c.SecurityProtocol = SecurityProtocol.SaslPlaintext;
            //     }); 
            //
            // opts.PublishAllMessages().ToKafkaTopics().UseDurableOutbox();
        });

        return builder;
    }
}