using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Orders.Domain.Order;

namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public interface IUnitOfWork
{
    IAggregateRepository<Order> Orders { get; }
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}