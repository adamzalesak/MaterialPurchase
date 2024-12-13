using System.Transactions;
using MaterialPurchase.Offers.Infrastructure;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;
using MaterialPurchase.Orders.Infrastructure.Persistence;

namespace MaterialPurchase.Wolverine;

public sealed class TransactionalMiddleware : IDisposable
{
    TransactionScope? _transactionScope;

    readonly OffersDbContext _offersDbContext;
    readonly OrderCartsDbContext _orderCartsDbContext;
    readonly OrdersDbContext _ordersDbContext;

    public TransactionalMiddleware(OffersDbContext offersDbContext, OrderCartsDbContext orderCartsDbContext,
        OrdersDbContext ordersDbContext)
    {
        _offersDbContext = offersDbContext;
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
        await _offersDbContext.SaveChangesAsync(cancellationToken);
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