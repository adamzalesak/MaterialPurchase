using MaterialPurchase.Offers.Application;
using MaterialPurchase.Offers.Domain.Offer.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Offers.Infrastructure;

public class ProductReadRepository(OffersDbContext dbContext) : IProductReadRepository
{
    public async Task<bool> CheckIfProductExists(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Products.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ICollection<ProductDto>> GetProductsByIds(ICollection<int> ids, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(x => ids.Contains(x.Id))
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
            })
            .ToListAsync(cancellationToken);
    }
}