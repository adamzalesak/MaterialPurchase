using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application;

public interface IUnitOfWork
{
    IAggregateRepository<OrderCart> OrderCarts { get; }
    IOrderCartReadModelRepository OrderCartReadModels { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}