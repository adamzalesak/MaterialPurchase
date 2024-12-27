using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.OrderCart;

namespace MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;

public static class FinishOrderCartCommandHandler
{
    public static async Task<OrderCart> Load(FinishOrderCartCommand command, IAggregateRepository<OrderCart> repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetById(command.OrderCartId, cancellationToken) ??
               throw new ArgumentException("Order cart not found");
    }
    
    public static void Handle(FinishOrderCartCommand command, OrderCart orderCart)
    {
        orderCart.Finish();
    }
}