using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;

public class FinishOrderCartCommandHandler : ICommandHandler<FinishOrderCartCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public FinishOrderCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(FinishOrderCartCommand command, CancellationToken cancellationToken)
    {
        var orderCart = await _unitOfWork.OrderCarts.GetById(command.OrderCartId, cancellationToken) ??
                        throw new ArgumentException("Order cart not found");

        orderCart.Finish();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}