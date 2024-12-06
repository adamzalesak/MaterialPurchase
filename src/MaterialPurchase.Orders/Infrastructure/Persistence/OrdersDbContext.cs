using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Orders.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options, IMessageBus bus) : DbContextBase(options, bus)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
    }
}