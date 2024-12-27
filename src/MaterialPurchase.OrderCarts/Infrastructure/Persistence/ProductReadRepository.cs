using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class ProductReadRepository(OrderCartsDbContext dbContext) : IProductReadRepository
{
    public async Task<ICollection<ProductDto>> GetAllProducts(CancellationToken cancellationToken)
    {
        return await dbContext.Products.ToListAsync(cancellationToken);
    }
    
    public async Task<ICollection<ProductDto>> GetProductsByIds(ICollection<int> ids, CancellationToken cancellationToken)
    {
        return await dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}