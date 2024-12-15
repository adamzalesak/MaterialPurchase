using MaterialPurchase.Offers.Application.Commands.SelectModels;
using MaterialPurchase.Offers.Application.Queries.GetOffers;

namespace MaterialPurchase.Offers.Application;

public interface IOfferReadRepository
{
    public Task<ICollection<GetOffersResponse>> GetOffers(CancellationToken cancellationToken);
    public Task<OfferSelectModel> GetOffer(Guid id, CancellationToken cancellationToken);
}