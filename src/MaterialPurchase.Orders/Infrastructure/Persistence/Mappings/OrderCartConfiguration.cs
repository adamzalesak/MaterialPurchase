using MaterialPurchase.Orders.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Orders.Infrastructure.Persistence.Mappings;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("OrderHeaders", "orders");
        builder.HasKey(orderCart => orderCart.Id);
    }
}