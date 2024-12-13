using MaterialPurchase.Common.Domain;
using MaterialPurchase.Common.Domain.ValueObjects;
using MaterialPurchase.Offers.Domain.Enums;
using MaterialPurchase.Offers.Entities;
using MaterialPurchase.OffersContracts.DomainEvents;

namespace MaterialPurchase.Offers.Domain;

public class Offer : AggregateRoot
{
    public int SupplierId { get; private set; }
    public OfferStatus Status { get; private set; }
    public DateTimeOffset ValidFrom { get; private set; }
    public DateTimeOffset? ValidTo { get; private set; }
    public string? Note { get; private set; }
    private List<OfferItem> _offerItems = [];
    public IReadOnlyCollection<OfferItem> OfferItems => _offerItems;

    Offer()
    {
    }

    public static Offer Create(int supplierId, DateTimeOffset validFrom, DateTimeOffset? validTo, string? note)
    {
        var offer = new Offer();
        offer.RaiseDomainEvent(new OfferCreatedDomainEvent
        {
            AggregateId = Guid.NewGuid(),
            SupplierId = supplierId,
            ValidFrom = validFrom,
            ValidTo = validTo,
            Note = note,
        });

        return offer;
    }

    public void AddOfferItem(int productId, int availableQuantity, Money price)
    {
        if (Status != OfferStatus.Draft)
        {
            throw new InvalidOperationException("Offer is not in draft status");
        }
        
        if (_offerItems.Exists(i => i.ProductId == productId))
        {
            throw new InvalidOperationException("Product already added to offer");
        }

        RaiseDomainEvent(new OfferItemAddedDomainEvent
        {
            OfferId = Id,
            OfferItemId = Guid.NewGuid(),
            ProductId = productId,
            AvailableQuantity = availableQuantity,
            Price = price,
        });
    }
    
    public void Confirm()
    {
        if (Status != OfferStatus.Draft)
        {
            throw new InvalidOperationException("Offer is not in draft status");
        }

        RaiseDomainEvent(new OfferConfirmedDomainEvent
        {
            OfferId = Id,
        });
    }

    public void ReserveQuantity(int productId, int quantity)
    {
        if (Status != OfferStatus.Confirmed)
        {
            throw new InvalidOperationException("Offer is not in confirmed status");
        }
        
        var offerItem = _offerItems.Find(i => i.ProductId == productId) ??
                        throw new InvalidOperationException("Product not found in offer");

        RaiseDomainEvent(new OfferItemQuantityReservedDomainEvent
        {
            OfferItemId = offerItem.Id,
            Quantity = quantity,
        });
    }

    public void Apply(OfferCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.AggregateId;
        SupplierId = domainEvent.SupplierId;
        ValidFrom = domainEvent.ValidFrom;
        ValidTo = domainEvent.ValidTo;
        Status = OfferStatus.Draft;
        Note = domainEvent.Note;
    }

    public void Apply(OfferItemAddedDomainEvent domainEvent)
    {
        _offerItems.Add(OfferItem.Create(
            offerId: domainEvent.OfferId,
            id: domainEvent.OfferItemId,
            productId: domainEvent.ProductId,
            availableQuantity: domainEvent.AvailableQuantity,
            price: domainEvent.Price
        ));
    }
    
    public void Apply(OfferConfirmedDomainEvent domainEvent)
    {
        Status = OfferStatus.Confirmed;
    }

    public void Apply(OfferItemQuantityReservedDomainEvent domainEvent)
    {
        var offerItem = _offerItems.Find(i => i.Id == domainEvent.OfferItemId) ??
                        throw new InvalidOperationException("Offer item not found");
        offerItem.ReserveQuantity(domainEvent.Quantity);
    }
}