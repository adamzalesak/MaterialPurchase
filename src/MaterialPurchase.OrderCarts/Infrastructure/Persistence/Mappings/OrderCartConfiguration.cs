using MaterialPurchase.OrderCarts.Domain.OrderCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence.Mappings;

public class OrderCartConfiguration : IEntityTypeConfiguration<OrderCart>
{
    public void Configure(EntityTypeBuilder<OrderCart> builder)
    {
        builder.ToTable("OrderCartHeaders");
        builder.HasKey(orderCart => orderCart.Id);
        builder.OwnsMany(orderCart => orderCart.Items, orderCartItem =>
        {
            orderCartItem.ToTable("OrderCartItems");
            orderCartItem.HasKey(x => x.Id);
            orderCartItem.Property(x => x.Id).ValueGeneratedNever();
            orderCartItem.WithOwner().HasForeignKey(x => x.OrderCartId);
            orderCartItem.OwnsOne(x => x.Price, price =>
            {
                price.Property(money => money.Amount).HasColumnName("PriceAmount").IsRequired().HasColumnType("money");
                price.Property(money => money.Currency).HasColumnName("PriceCurrency").IsRequired().HasMaxLength(3);
            });
        });
    }
}