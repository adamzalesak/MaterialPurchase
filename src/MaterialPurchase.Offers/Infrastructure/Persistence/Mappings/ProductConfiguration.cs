using MaterialPurchase.Offers.Domain.Offer.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.Offers.Infrastructure.Persistence.Mappings;

public class ProductConfiguration : IEntityTypeConfiguration<ProductDto>
{
    public void Configure(EntityTypeBuilder<ProductDto> builder)
    {
        builder.ToTable("Products", "dbo");
    }
}