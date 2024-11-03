using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;

public record FinishOrderCartCommand(Guid OrderCartId) : ICommand;