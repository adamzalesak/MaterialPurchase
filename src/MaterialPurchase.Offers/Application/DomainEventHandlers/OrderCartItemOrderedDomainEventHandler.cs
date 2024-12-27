using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.Offers.Application.DomainEventHandlers;

public static class OrderCartItemOrderedDomainEventHandler
{
    public static async Task<Offer> Load(OrderCartItemOrderedDomainEvent @event, IAggregateRepository<Offer> repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetById(@event.OfferId, cancellationToken) ??
               throw new ArgumentException("Offer not found");
    }

    public static void Handle(OrderCartItemOrderedDomainEvent domainEvent, Offer offer)
    {
        offer.ReserveItemQuantity(domainEvent.ProductId, domainEvent.Quantity);
    }
}