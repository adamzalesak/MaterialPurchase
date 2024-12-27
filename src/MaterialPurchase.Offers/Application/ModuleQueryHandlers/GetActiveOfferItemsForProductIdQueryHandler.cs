using MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;

namespace MaterialPurchase.Offers.Application.ModuleQueryHandlers;

public static class GetActiveOfferItemsForProductIdQueryHandler
{
    public static async Task<GetActiveOfferItemsForProductIdResponse> Handle(GetActiveOfferItemsForProductIdQuery query,
        IOfferReadRepository offerReadRepository, CancellationToken cancellationToken)
    {
        var offerItems = await offerReadRepository.GetActiveOfferItemsForProductId(query.ProductId, cancellationToken);

        return new GetActiveOfferItemsForProductIdResponse
        {
            OfferItems = offerItems,
        };
    }
}