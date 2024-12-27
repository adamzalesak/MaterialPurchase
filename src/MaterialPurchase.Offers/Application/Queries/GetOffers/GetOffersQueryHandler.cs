namespace MaterialPurchase.Offers.Application.Queries.GetOffers;

public static class GetOffersQueryHandler
{
    public static async Task<ICollection<GetOffersResponse>> Handle(GetOffersQuery query, IOfferReadRepository offerReadRepository,
        CancellationToken cancellationToken)
    {
        return await offerReadRepository.GetOffers(cancellationToken);
    }
}