namespace MaterialPurchase.Common.Infrastructure.Kafka;

public record KafkaConfig
{
    public required string BootstrapServers { get; set; }
    public required string SaslUsername { get; set; }
    public required string SaslPassword { get; set; }
}