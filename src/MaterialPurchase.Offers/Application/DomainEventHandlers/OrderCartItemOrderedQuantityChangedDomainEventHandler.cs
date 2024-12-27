using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.Offers.Application.DomainEventHandlers;

public static class OrderCartItemOrderedQuantityChangedDomainEventHandler
{
    public static async Task<Offer> Load(OrderCartItemOrderedQuantityChangedDomainEvent @event, IAggregateRepository<Offer> repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetById(@event.OfferId, cancellationToken)
               ?? throw new ArgumentException("Offer not found");
    }

    public static void Handle(OrderCartItemOrderedQuantityChangedDomainEvent @event, Offer offer)
    {
        offer.ChangeItemReservedQuantity(@event.ProductId, @event.OriginalQuantity, @event.Quantity);
    }
}