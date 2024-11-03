using MaterialPurchase.Common.Application.CommandsAndQueries;

namespace MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;

public record GetOrderCartsQuery : IQuery<ICollection<GetOrderCartsResponse>>;