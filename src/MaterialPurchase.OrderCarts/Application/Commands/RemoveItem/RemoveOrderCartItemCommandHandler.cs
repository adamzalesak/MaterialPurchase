using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application.Commands.RemoveItem;

public class RemoveOrderCartItemCommandHandler
{
    public async Task<OrderCart> Load(RemoveOrderCartItemCommand command, IAggregateRepository<OrderCart> repository,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
               throw new ArgumentException("Order cart not found");
        return orderCart;
    }
    
    public void Handle(RemoveOrderCartItemCommand command, OrderCart orderCart)
    {
        orderCart.RemoveItem(command.OrderCartItemId);
    }
}