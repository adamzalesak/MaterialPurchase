using MaterialPurchase.Offers.Application;
using MaterialPurchase.Offers.Application.Queries.GetOffers;
using MaterialPurchase.Offers.Application.SelectModels;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;
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
            .AsNoTracking()
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
            .AsNoTracking()
            .FirstAsync(cancellationToken);

        return offer;
    }

    public async Task<ICollection<ActiveOfferItemForProductIdDto>> GetActiveOfferItemsForProductId(int productId, CancellationToken cancellationToken)
    {
        return await dbContext.Offers
            .Where(o => o.Status == OfferStatus.Confirmed)
            .SelectMany(o => o.OfferItems
                .Where(oi => oi.ProductId == productId)
                .Select(oi =>
                    new ActiveOfferItemForProductIdDto
                    {
                        OfferId = oi.OfferId,
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        Price = oi.Price,
                        AvailableQuantity = oi.AvailableQuantity,
                        SupplierId = o.SupplierId,
                    })
                .ToList())
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}