using MaterialPurchase.Orders.Application.Queries.GetOrders;
using MaterialPurchase.Orders.Application.SelectModels;

namespace MaterialPurchase.Orders.Application;

public interface IOrderReadRepository
{
    public Task<bool> ExistsByOrderCartId(Guid orderCartId, CancellationToken cancellationToken);
    public Task<OrderSelectModel?> GetByOrderCartId(Guid id, CancellationToken cancellationToken);
    public Task<ICollection<GetOrdersResponse>> GetOrders(CancellationToken cancellationToken);
}