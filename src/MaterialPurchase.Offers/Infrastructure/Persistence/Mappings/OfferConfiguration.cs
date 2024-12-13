using MaterialPurchase.Offers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Offers.Infrastructure.Persistence.Mappings;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("OfferHeaders");
        builder.HasKey(orderCart => orderCart.Id);
        builder.OwnsMany(orderCart => orderCart.OfferItems, orderCartItem =>
        {
            orderCartItem.ToTable("OfferItems");
            orderCartItem.WithOwner().HasForeignKey(x => x.OfferId);
            orderCartItem.OwnsOne(x => x.Price, price =>
            {
                price.Property(x => x.Amount).HasColumnName("Price").IsRequired();
                price.Property(x => x.Currency).HasColumnName("Currency").IsRequired();
            });
        });
    }
}