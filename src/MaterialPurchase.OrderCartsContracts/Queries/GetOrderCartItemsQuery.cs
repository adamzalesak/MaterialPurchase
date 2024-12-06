using MaterialPurchase.Common.Application;

namespace MaterialPurchase.OrderCartsContracts.Queries;

public record GetOrderCartItemsQuery(Guid OrderCartId) : IQuery;