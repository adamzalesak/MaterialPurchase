using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;
using MaterialPurchase.OrderCarts.Application.SelectModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class OrderCartReadRepository(OrderCartsDbContext dbContext) : IOrderCartReadRepository
{
    public async Task<OrderCartSelectModel?> GetOrderCart(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.OrderCarts
            .Where(x => x.Id == id)
            .Select(x => new OrderCartSelectModel
            {
                Id = x.Id, Name = x.Name, Status = x.Status, Items = x.Items.Select(i => new OrderCartItemSelectModel
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    SupplierId = i.SupplierId,
                    Price = i.Price,
                    OfferId = i.OfferId,
                }).ToList(),
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<GetOrderCartsResponse>> GetOrderCarts(CancellationToken cancellationToken)
    {
        return await dbContext.OrderCarts
            .Select(x => new GetOrderCartsResponse { Id = x.Id, Name = x.Name, Status = x.Status.ToString() })
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderCartStatsSelectModel?> GetOrderCartStats(CancellationToken cancellationToken)
    {
        return await dbContext.OrderCartStatsReadModels
            .Select(x =>
                new OrderCartStatsSelectModel
                {
                    CreatedCount = x.CreatedCount,
                    FinishedCount = x.FinishedCount,
                })
            .FirstOrDefaultAsync(cancellationToken);
    }
}