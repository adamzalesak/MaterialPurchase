using MaterialPurchase.Offers.Application.Commands.SelectModels;
using MaterialPurchase.Offers.Application.Queries.GetOffers;
using MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;

namespace MaterialPurchase.Offers.Application;

public interface IOfferReadRepository
{
    public Task<ICollection<GetOffersResponse>> GetOffers(CancellationToken cancellationToken);
    public Task<OfferSelectModel> GetOffer(Guid id, CancellationToken cancellationToken);
    public Task<ICollection<ActiveOfferItemForProductIdDto>> GetActiveOfferItemsForProductId(int productId, CancellationToken cancellationToken);
}