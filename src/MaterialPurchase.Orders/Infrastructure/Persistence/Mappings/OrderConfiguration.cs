using MaterialPurchase.Orders.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Orders.Infrastructure.Persistence.Mappings;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("OrderHeaders");
        builder.HasKey(orderCart => orderCart.Id);
        builder.Property(orderCart => orderCart.Id).ValueGeneratedNever();
        builder.Property(orderCart => orderCart.Status).IsRequired();
        builder.OwnsMany(orderCart => orderCart.Items, items =>
        {
            items.ToTable("OrderItems");
            items.HasKey(item => item.Id);
            items.Property(item => item.Id).ValueGeneratedNever();
            items.OwnsOne(item => item.Price, price =>
            {
                price.Property(money => money.Amount).HasColumnName("PriceAmount").IsRequired();
                price.Property(money => money.Currency).HasColumnName("PriceCurrency").IsRequired();
            });
        });
    }
}