using MaterialPurchase.Offers.Domain.Offer.Dtos;

namespace MaterialPurchase.Offers.Application;

public interface IProductReadRepository
{
    public Task<bool> CheckIfProductExists(int id, CancellationToken cancellationToken);
    public Task<ICollection<ProductDto>> GetProductsByIds(ICollection<int> ids, CancellationToken cancellationToken);
}