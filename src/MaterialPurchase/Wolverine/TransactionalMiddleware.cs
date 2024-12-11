using System.Transactions;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;
using MaterialPurchase.Orders.Infrastructure.Persistence;

namespace MaterialPurchase.Wolverine;

public sealed class TransactionalMiddleware : IDisposable
{
    TransactionScope? _transactionScope;

    readonly OrderCartsDbContext _orderCartsDbContext;
    readonly OrdersDbContext _ordersDbContext;

    public TransactionalMiddleware(OrderCartsDbContext orderCartsDbContext, OrdersDbContext ordersDbContext)
    {
        _orderCartsDbContext = orderCartsDbContext;
        _ordersDbContext = ordersDbContext;
    }

    public void Before()
    {
        _transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
    }

    public async Task After(CancellationToken cancellationToken)
    {
        await _orderCartsDbContext.SaveChangesAsync(cancellationToken);
        await _ordersDbContext.SaveChangesAsync(cancellationToken);

        _transactionScope?.Complete();
    }

    public void Finally()
    {
        _transactionScope?.Dispose();
    }

    public void Dispose()
    {
        _transactionScope?.Dispose();
    }
}