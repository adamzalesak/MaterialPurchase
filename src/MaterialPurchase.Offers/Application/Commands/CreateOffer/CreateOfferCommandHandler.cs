using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Offers.Domain;
using MaterialPurchase.Offers.Domain.Offer;

namespace MaterialPurchase.Offers.Application.Commands.CreateOffer;

public static class CreateOfferCommandHandler
{
    public static Guid Handle(CreateOfferCommand command, IAggregateRepository<Offer> repository)
    {
        var offer = Offer.Create(
            supplierId: command.SupplierId,
            validFrom: command.ValidFrom,
            validTo: command.ValidTo,
            note: command.Note
        );

        repository.Add(offer);

        return offer.Id;
    }
}