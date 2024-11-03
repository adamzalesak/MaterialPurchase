using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public class OrderReadRepository(OrdersDbContext dbContext) : IOrderReadRepository
{
    public async Task<bool> ExistsByOrderCartId(Guid orderCartId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders.AnyAsync(o => o.OrderCartId == orderCartId, cancellationToken);
    }
}