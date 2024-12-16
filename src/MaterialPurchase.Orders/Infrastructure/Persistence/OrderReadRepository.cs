using MaterialPurchase.Orders.Application;
using MaterialPurchase.Orders.Application.Queries.GetOrders;
using MaterialPurchase.Orders.Application.SelectModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public class OrderReadRepository(OrdersDbContext dbContext) : IOrderReadRepository
{
    public async Task<bool> ExistsByOrderCartId(Guid orderCartId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders.AnyAsync(o => o.OrderCartId == orderCartId, cancellationToken);
    }

    public async Task<OrderSelectModel?> GetByOrderCartId(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderSelectModel
            {
                OrderId = o.Id,
                OrderCartId = o.OrderCartId,
                Status = o.Status,
                SupplierId = o.SupplierId,
                Items = o.Items.Select(oi => new OrderItemSelectModel
                {
                    OrderItemId = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Name = oi.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                }).ToList(),
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<GetOrdersResponse>> GetOrders(CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .Select(o => new GetOrdersResponse
            {
                OrderId = o.Id,
                OrderCartId = o.OrderCartId,
                Status = o.Status.ToString(),
                SupplierId = o.SupplierId,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}