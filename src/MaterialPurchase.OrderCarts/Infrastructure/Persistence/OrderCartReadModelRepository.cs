using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Application.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class OrderCartReadModelRepository(OrderCartsDbContext dbContext) : IOrderCartReadModelRepository
{
    public Task<OrderCartStatsReadModel?> GetOrderCartStats(CancellationToken cancellationToken)
    {
        return dbContext.OrderCartStatsReadModels.FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(OrderCartStatsReadModel entity) => dbContext.Add(entity);
}