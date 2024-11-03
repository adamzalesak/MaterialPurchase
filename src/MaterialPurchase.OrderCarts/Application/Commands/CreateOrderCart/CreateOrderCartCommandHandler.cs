using MaterialPurchase.Common.Application.CommandsAndQueries;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public class CreateOrderCartCommandHandler : ICommandHandler<CreateOrderCartCommand, Guid>
{
    readonly IUnitOfWork _unitOfWork;

    public CreateOrderCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCartCommand command, CancellationToken cancellationToken)
    {
        var orderCart = OrderCart.Create(command.Name);

        _unitOfWork.OrderCarts.Add(orderCart);

        /*
        // you can publish messages like this (but this message is already created in the domain
        // and will be published automatically by DbContextBase SaveChangesAsync override)
        var @event = new OrderCartCreatedDomainEvent(orderCart.Id);
        await _bus.PublishAsync(@event, new DeliveryOptions { PartitionKey = orderCart.Id.ToString() });
        */

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return orderCart.Id;
    }
}