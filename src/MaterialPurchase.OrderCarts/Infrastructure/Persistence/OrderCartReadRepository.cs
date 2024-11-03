using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Application.SelectModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class OrderCartReadRepository(OrderCartsDbContext dbContext) : IOrderCartReadRepository
{
    public async Task<OrderCartSelectModel?> GetOrderCart(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.OrderCarts
            .Where(x => x.Id == id)
            .Select(x => new OrderCartSelectModel { Id = x.Id, Name = x.Name })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<OrderCartSelectModel>> GetOrderCarts(CancellationToken cancellationToken)
    {
        return await dbContext.OrderCarts
            .Select(x => new OrderCartSelectModel { Id = x.Id, Name = x.Name })
            .ToListAsync(cancellationToken);
    }
}