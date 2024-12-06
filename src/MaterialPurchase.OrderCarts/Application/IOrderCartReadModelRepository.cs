using MaterialPurchase.OrderCarts.Application.ReadModels;

namespace MaterialPurchase.OrderCarts.Application;

public interface IOrderCartReadModelRepository
{
    public Task<OrderCartStatsReadModel?> GetOrderCartStats(CancellationToken cancellationToken);
    public void Add(OrderCartStatsReadModel entity);
}