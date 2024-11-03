using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;

public record CreateOrderCartCommand(string Name) : ICommand<Guid>;