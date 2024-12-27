using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain;
using MaterialPurchase.Offers.Domain.Offer;

namespace MaterialPurchase.Offers.Application.Commands.ConfirmOffer;

public static class ConfirmOfferCommandHandler
{
    public static async Task<Offer> Load(ConfirmOfferCommand command, IAggregateRepository<Offer> repository, CancellationToken cancellationToken)
    {
        var offer = await repository.GetById(command.OfferId, cancellationToken)
                    ?? throw new ArgumentException("Offer not found");
        return offer;
    }

    public static void Handle(ConfirmOfferCommand command, Offer offer)
    {
        offer.Confirm();
    }
}