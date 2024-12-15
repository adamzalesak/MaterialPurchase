using MaterialPurchase.Common.Application;

namespace MaterialPurchase.Offers.Application.Queries.GetOffer;

public record GetOfferQuery(Guid OfferId) : IQuery;