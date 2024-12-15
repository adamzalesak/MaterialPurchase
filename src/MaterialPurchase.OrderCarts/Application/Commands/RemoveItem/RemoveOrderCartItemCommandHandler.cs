using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.OrderCart;

namespace MaterialPurchase.OrderCarts.Application.Commands.RemoveItem;

public static class RemoveOrderCartItemCommandHandler
{
    public static async Task<OrderCart> Load(RemoveOrderCartItemCommand command, IAggregateRepository<OrderCart> repository,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
               throw new ArgumentException("Order cart not found");
        return orderCart;
    }
    
    public static void Handle(RemoveOrderCartItemCommand command, OrderCart orderCart)
    {
        orderCart.RemoveItem(command.OrderCartItemId);
    }
}