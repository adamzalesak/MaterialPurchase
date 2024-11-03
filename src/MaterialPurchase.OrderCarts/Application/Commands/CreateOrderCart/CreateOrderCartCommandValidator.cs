using FluentValidation;

namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public class CreateOrderCartCommandValidator : AbstractValidator<CreateOrderCartCommand>
{
    public CreateOrderCartCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}