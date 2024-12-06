using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public record OrderProductCommand : ICommand
{
    public int ProductId { get; init; }
    public Guid OfferId { get; init; }
    
}