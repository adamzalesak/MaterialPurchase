using MaterialPurchase.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public class CommonDbContext(DbContextOptions<CommonDbContext> options, IMessageBus bus) : DbContextBase(options, bus)
{
    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommonDbContext).Assembly);
    }
}