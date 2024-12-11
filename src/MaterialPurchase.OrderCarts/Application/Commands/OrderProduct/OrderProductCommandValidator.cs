using FluentValidation;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public class OrderProductCommandValidator : AbstractValidator<OrderProductCommand>
{
    public OrderProductCommandValidator()
    {
        RuleFor(x => x.OrderCartId).NotEmpty();
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.OfferId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}