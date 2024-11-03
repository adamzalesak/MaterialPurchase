using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.Orders.Domain.Order;

namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public class UnitOfWork(OrdersDbContext dbContext, IAggregateRepository<Order> orders) : UnitOfWorkBase(dbContext), IUnitOfWork
{
    public IAggregateRepository<Order> Orders { get; } = orders;
}