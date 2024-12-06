using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;

public class FinishOrderCartCommandHandler
{
    public async Task Handle(FinishOrderCartCommand command, IAggregateRepository<OrderCart> repository,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
                        throw new ArgumentException("Order cart not found");

        orderCart.Finish();
    }
}