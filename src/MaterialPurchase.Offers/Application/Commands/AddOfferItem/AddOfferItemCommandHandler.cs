using MaterialPurchase.Common.Domain.ValueObjects;
using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain;

namespace MaterialPurchase.Offers.Application.Commands.AddOfferItem;

public static class AddOfferItemCommandHandler
{
    public static async Task<Offer> Load(AddOfferItemCommand command, IAggregateRepository<Offer> repository, CancellationToken cancellationToken)
    {
        var offer = await repository.GetById(command.OfferId, cancellationToken)
                    ?? throw new ArgumentException("Offer not found");
        return offer;
    }

    public static void Handle(AddOfferItemCommand command, Offer offer)
    {
        offer.AddOfferItem(
            productId: command.ProductId,
            availableQuantity: command.AvailableQuantity,
            price: new Money(command.Price, command.CurrencyCode)
        );
    }
}