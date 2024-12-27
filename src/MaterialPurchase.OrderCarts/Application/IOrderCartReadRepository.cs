using MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;
using MaterialPurchase.OrderCarts.Application.SelectModels;

namespace MaterialPurchase.OrderCarts.Application;

public interface IOrderCartReadRepository
{
    public Task<OrderCartSelectModel?> GetOrderCart(Guid id, CancellationToken cancellationToken);
    public Task<ICollection<GetOrderCartsResponse>> GetOrderCarts(CancellationToken cancellationToken);
    public Task<OrderCartStatsSelectModel?> GetOrderCartStats(CancellationToken cancellationToken);
}