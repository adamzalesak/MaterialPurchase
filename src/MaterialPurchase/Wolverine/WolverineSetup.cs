using Confluent.Kafka;
using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Commands;
using JasperFx.Core;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.Orders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Kafka;
using Wolverine.SqlServer;

namespace MaterialPurchase.Wolverine;

public static class WolverineSetup
{
    const string OrderCartQueueName = "orderCart";
    const string OrderQueueName = "order";

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

            opts.UseKafka("pkc-56d1g.eastus.azure.confluent.cloud:9092")
                .ConfigureClient(c =>
                {
                    c.SaslUsername = "D6ULSE5C46WQJLVW";
                    c.SaslPassword = "49jQnKDLREXR9Qrpue9+vxqsItxW/TaYDwsAO6ulgg+mqr+aHubjVecWaL+vvLTF";
                    c.SaslMechanism = SaslMechanism.Plain;
                    c.SecurityProtocol = SecurityProtocol.SaslSsl;
                })
                .ConfigureProducers(c => { c.EnableIdempotence = true; })
                .ConfigureConsumers(c =>
                {
                    c.EnableAutoCommit = true;
                    c.EnableAutoOffsetStore = true;
                    // je potřeba nastavit  DeliveryGuarantee.EXACTLY_ONCE?
                })
                ;

            // Pro každý agregát bude potřeba nastavit publikování do vlastní local queue se strict ordering,
            // aby bylo možné paralelizovat aspoň na úrovni agregátů
            opts.Publish(c =>
            {
                c.MessagesImplementing<IOrderCartDomainEvent>();
                c.ToLocalQueue(OrderCartQueueName).ListenWithStrictOrdering();
            });

            // tady je MessageBatchMaxDegreeOfParallelism by default 1, což znamená, že zprávy jsou seřazené
            // opts.PublishMessage<OrderCartCreatedDomainEvent>().ToKafkaTopic("topic_0");
            // opts.Publish(c =>
            // {
            //     c.Message<OrderCartCreatedDomainEvent>();
            //     c.Message<OrderCartFinishedDomainEvent>();
            //     c.ToKafkaTopic("topic_0");
            // });

            // tady by mělo stačit sequential a ne strict ordering, protože kafka bude posílat jen jednu partition
            // seřazených zpráv (není garance pořadí napříč partitions);
            // opts.ListenToKafkaTopic("topic_0").Sequential();
        });

        return builder;
    }
}