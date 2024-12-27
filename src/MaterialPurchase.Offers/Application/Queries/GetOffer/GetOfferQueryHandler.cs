namespace MaterialPurchase.Offers.Application.Queries.GetOffer;

public static class GetOfferQueryHandler
{
    public static async Task<GetOfferResponse> Handle(GetOfferQuery query, IOfferReadRepository offerReadRepository,
        IProductReadRepository productReadRepository,
        CancellationToken cancellationToken)
    {
        var offer = await offerReadRepository.GetOffer(query.OfferId, cancellationToken);

        var productIds = offer.Items.Select(x => x.ProductId).Distinct().ToList();
        var products =
            await productReadRepository.GetProductsByIds(productIds.ToList(), cancellationToken);

        var response = new GetOfferResponse
        {
            Id = offer.Id,
            Status = offer.Status,
            ValidTo = offer.ValidTo,
            ValidFrom = offer.ValidFrom,
            Note = offer.Note,
            Items = offer.Items.Select(x =>
            {
                var product = products.First(p => p.Id == x.ProductId);

                return new OfferItemDto
                {
                    OfferId = x.OfferId,
                    Id = x.Id,
                    Price = x.Price,
                    Currency = x.Currency,
                    AvailableQuantity = x.AvailableQuantity,
                    ProductId = product.Id,
                    ProductCode = product.Code,
                    ProductName = product.Name,
                };
            }).ToList(),
        };

        return response;
    }
}