using MaterialPurchase.Common.Domain;
using MaterialPurchase.Offers.Entities;

namespace MaterialPurchase.Offers.Domain;

public class Offer : AggregateRoot
{
    private List<OfferItem> _offerItems = [];
    public IReadOnlyCollection<OfferItem> OfferItems => _offerItems;

}