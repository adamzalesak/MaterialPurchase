using MaterialPurchase.OrderCarts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence.Mappings;

public class OrderCartConfiguration : IEntityTypeConfiguration<OrderCart>
{
    public void Configure(EntityTypeBuilder<OrderCart> builder)
    {
        builder.ToTable("OrderCartHeaders", "orderCarts");
        builder.HasKey(orderCart => orderCart.Id);
        builder.OwnsMany(orderCart => orderCart.OrderCartItems, orderCartItem =>
        {
            orderCartItem.ToTable("OrderCartItems", "orderCarts");
            orderCartItem.WithOwner().HasForeignKey(x => x.OrderCartId);
        });
    }
}