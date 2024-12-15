using MaterialPurchase.Offers.Application;
using MaterialPurchase.Offers.Application.Commands.SelectModels;
using MaterialPurchase.Offers.Application.Queries.GetOffer;
using MaterialPurchase.Offers.Application.Queries.GetOffers;
using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Offers.Infrastructure;

public class OfferReadRepository(OffersDbContext dbContext) : IOfferReadRepository
{
    public async Task<ICollection<GetOffersResponse>> GetOffers(CancellationToken cancellationToken)
    {
        var offers = await dbContext.Offers
            .Select(x => new GetOffersResponse
            {
                Id = x.Id,
                Status = x.Status.ToString(),
                ValidTo = x.ValidTo,
                ValidFrom = x.ValidFrom,
                Note = x.Note,
            })
            .ToListAsync(cancellationToken);

        return offers;
    }

    public async Task<OfferSelectModel> GetOffer(Guid id, CancellationToken cancellationToken)
    {
        var offer = await dbContext.Offers
            .Where(x => x.Id == id)
            .Select(x => new OfferSelectModel
            {
                Id = x.Id,
                Status = x.Status.ToString(),
                ValidTo = x.ValidTo,
                ValidFrom = x.ValidFrom,
                Note = x.Note,
                Items = x.OfferItems.Select(y => new OfferItemSelectModel
                {
                    OfferId = y.OfferId,
                    Id = y.Id,
                    Price = y.Price.Amount,
                    Currency = y.Price.Currency,
                    AvailableQuantity = y.AvailableQuantity,
                    ProductId = y.ProductId,
                }).ToList(),
            })
            .FirstAsync(cancellationToken);

        return offer;
    }
}