using MaterialPurchase.OrderCarts.Domain.Dtos;

namespace MaterialPurchase.OrderCarts.Application;

public interface IProductReadRepository
{
    public Task<ICollection<ProductDto>> GetAllProducts(CancellationToken cancellationToken);
    public Task<ICollection<ProductDto>> GetProductsByIds(ICollection<int> ids, CancellationToken cancellationToken);
}