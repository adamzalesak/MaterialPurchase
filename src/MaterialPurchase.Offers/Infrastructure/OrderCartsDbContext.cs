using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.Offers.Domain.Offer.Dtos;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.Offers.Infrastructure;

public class OffersDbContext(DbContextOptions<OffersDbContext> options, IMessageBus bus) : DbContextBase(options, bus)
{
    public DbSet<Offer> Offers { get; set; }

    public DbSet<ProductDto> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("offers");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OffersDbContext).Assembly);
    }
}