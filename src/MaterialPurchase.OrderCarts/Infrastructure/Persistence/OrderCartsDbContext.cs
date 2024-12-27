using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Application.ReadModels;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class OrderCartsDbContext(DbContextOptions<OrderCartsDbContext> options, IMessageBus bus) : DbContextBase(options, bus)
{
    public DbSet<OrderCart> OrderCarts { get; set; }
    public DbSet<OrderCartStatsReadModel> OrderCartStatsReadModels { get; set; }

    public DbSet<ProductDto> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orderCarts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderCartsDbContext).Assembly);
    }
}