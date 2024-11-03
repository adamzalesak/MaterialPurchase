using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Application;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Infrastructure.Persistence;

public class UnitOfWork(OrderCartsDbContext dbContext, IAggregateRepository<OrderCart> orderCarts) : UnitOfWorkBase(dbContext), IUnitOfWork
{
    public IAggregateRepository<OrderCart> OrderCarts { get; } = orderCarts;
}