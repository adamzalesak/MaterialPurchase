using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence.Mappings;

public class ProductConfiguration : IEntityTypeConfiguration<ProductDto>
{
    public void Configure(EntityTypeBuilder<ProductDto> builder)
    {
        builder.ToTable("Products", "dbo");
    }
}