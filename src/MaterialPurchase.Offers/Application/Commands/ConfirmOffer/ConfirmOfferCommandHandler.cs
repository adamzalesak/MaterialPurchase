using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain;

namespace MaterialPurchase.Offers.Application.Commands.ConfirmOffer;

public class ConfirmOfferCommandHandler
{
    public async Task<Offer> Load(ConfirmOfferCommand command, IAggregateRepository<Offer> repository, CancellationToken cancellationToken)
    {
        var offer = await repository.GetById(command.OfferId, cancellationToken)
                    ?? throw new ArgumentException("Offer not found");
        return offer;
    }

    public void Handle(ConfirmOfferCommand command, Offer offer)
    {
        offer.Confirm();
    }
}