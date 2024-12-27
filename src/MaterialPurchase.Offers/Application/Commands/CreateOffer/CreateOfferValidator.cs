using FluentValidation;

namespace MaterialPurchase.Offers.Application.Commands.CreateOffer;

public class CreateOfferValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferValidator()
    {
        RuleFor(x => x.SupplierId).GreaterThan(0);
    }
}