using MaterialPurchase.Common.Application;

namespace MaterialPurchase.OrderCartsContracts.ModuleQueries;

public record GetOrderCartItemsQuery(Guid OrderCartId) : IQuery;