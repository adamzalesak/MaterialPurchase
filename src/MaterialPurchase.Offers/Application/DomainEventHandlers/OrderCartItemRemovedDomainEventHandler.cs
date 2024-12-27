using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.Offers.Application.DomainEventHandlers;

public static class OrderCartItemRemovedDomainEventHandler
{
    public static async Task<Offer> Load(OrderCartItemRemovedDomainEvent @event, IAggregateRepository<Offer> repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetById(@event.OfferId, cancellationToken) ??
               throw new ArgumentException("Offer not found");
    }
    
    public static void Handle(OrderCartItemRemovedDomainEvent @event, Offer offer)
    {
        offer.ChangeItemReservedQuantity(@event.ProductId, @event.OriginalQuantity, 0);
    }
}