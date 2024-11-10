using MaterialPurchase.OrderCarts.Application.Entities;

namespace MaterialPurchase.OrderCarts.Application;

public interface IProductReadRepository
{
    public Task<ICollection<Product>> GetAllProducts(CancellationToken cancellationToken);
    public Task<ICollection<Product>> GetProductsByIds(ICollection<int> ids, CancellationToken cancellationToken);
}