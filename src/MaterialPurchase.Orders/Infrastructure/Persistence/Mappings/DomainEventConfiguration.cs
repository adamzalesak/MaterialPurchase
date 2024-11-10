using MaterialPurchase.Common.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Orders.Infrastructure.Persistence.Mappings;

public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEventEnvelope>
{
    public void Configure(EntityTypeBuilder<DomainEventEnvelope> builder)
    {
        builder.ToTable("DomainEvents", "orders");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.AggregateType).IsRequired();
        builder.Property(e => e.AggregateId).IsRequired();
        builder.Property(e => e.OccurredOn).IsRequired();
        builder.Property(e => e.Data).IsRequired();
        builder.Property(e => e.EventType).IsRequired();
    }
}