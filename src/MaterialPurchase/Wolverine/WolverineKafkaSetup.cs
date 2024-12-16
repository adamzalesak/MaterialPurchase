using Confluent.Kafka;
using Wolverine;
using Wolverine.Kafka;

namespace MaterialPurchase.Wolverine;

public static class WolverineKafkaSetup
{
    public static WolverineOptions SetupKafka(this WolverineOptions opts, WebApplicationBuilder builder)
    {
        var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfig>();
        if (kafkaConfig is not null && kafkaConfig.BootstrapServers != string.Empty)
        {
            opts.UseKafka(kafkaConfig.BootstrapServers)
                .ConfigureClient(c =>
                {
                    c.SaslUsername = kafkaConfig.SaslUsername;
                    c.SaslPassword = kafkaConfig.SaslPassword;
                    c.SaslMechanism = SaslMechanism.Plain;
                    c.SecurityProtocol = SecurityProtocol.SaslSsl;
                })
                .ConfigureProducers(c => { c.EnableIdempotence = true; });
        }


        // MessageBatchMaxDegreeOfParallelism is 1 by default, which means that messages are published sequentially

        // opts.Publish(c =>
        // {
        //     c.MessagesImplementing<IOrderCartIntegrationEvent>();
        //     c.ToKafkaTopic("MaterialPurchase.OrderCartIntegrationEvents");
        // });


        // Kafka sends messages belonging to a single partition, ordered within that partition.
        // So consuming sequentially is enough to ensure ordering.

        // opts.ListenToKafkaTopic("Catalog.ProductChanged").Sequential();

        return opts;
    }
}