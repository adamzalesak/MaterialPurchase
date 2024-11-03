using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class OrderCartsDbContext(DbContextOptions<OrderCartsDbContext> options, IMessageBus bus) : DbContextBase(options, bus)
{
    public DbSet<OrderCart> OrderCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderCartsDbContext).Assembly);
    }
}