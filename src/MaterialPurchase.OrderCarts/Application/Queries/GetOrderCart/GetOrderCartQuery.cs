using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;

public record GetOrderCartQuery(Guid Id) : IQuery<GetOrderCartResponse?>;