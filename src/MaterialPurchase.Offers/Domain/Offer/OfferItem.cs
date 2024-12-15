using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.Offers.Domain.Offer;

public class OfferItem : Entity<Guid>
{
    public Guid OfferId { get; private set; }
    public int ProductId { get; private set; }
    public Money Price { get; private set; } = default!;
    public int? AvailableQuantity { get; private set; }

    private OfferItem()
    {
    }

    public static OfferItem Create(Guid id, Guid offerId, int productId, int? availableQuantity, Money price)
    {
        return new OfferItem
        {
            Id = id,
            OfferId = offerId,
            ProductId = productId,
            AvailableQuantity = availableQuantity,
            Price = price,
        };
    }

    internal void ChangeAvailableQuantity(int quantity)
    {
        AvailableQuantity = quantity;
    }
}