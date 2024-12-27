using MaterialPurchase.Offers.Domain;
using MaterialPurchase.Offers.Domain.Offer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Offers.Infrastructure.Persistence.Mappings;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("OfferHeaders");
        builder.HasKey(offer => offer.Id);
        builder.OwnsMany(offer => offer.OfferItems, offerItem =>
        {
            offerItem.ToTable("OfferItems");
            offerItem.HasKey(x => x.Id);
            offerItem.Property(x => x.Id).ValueGeneratedNever();
            offerItem.WithOwner().HasForeignKey(x => x.OfferId);
            offerItem.OwnsOne(x => x.Price, price =>
            {
                price.Property(x => x.Amount).HasColumnName("PriceAmount").IsRequired().HasColumnType("money");
                price.Property(x => x.Currency).HasColumnName("PriceCurrency").IsRequired().HasMaxLength(3);
            });
        });
    }
}