using FluentValidation;

namespace MaterialPurchase.Offers.Application.Commands.AddOfferItem;

public class AddOfferItemCommandValidator : AbstractValidator<AddOfferItemCommand>
{
    public AddOfferItemCommandValidator()
    {
        RuleFor(x => x.OfferId).NotEmpty();
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.CurrencyCode).Length(3);
    }
}