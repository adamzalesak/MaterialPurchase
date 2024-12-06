using MaterialPurchase.Common.Application;
using System.Transactions;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;
using MaterialPurchase.Orders.Infrastructure.Persistence;
using Wolverine;

namespace MaterialPurchase.Wolverine;

public sealed class TransactionScopeMiddleware : IDisposable
{
    TransactionScope? _transactionScope;

    readonly OrderCartsDbContext _orderCartsDbContext;
    readonly OrdersDbContext _ordersDbContext;

    public TransactionScopeMiddleware(OrderCartsDbContext orderCartsDbContext, OrdersDbContext ordersDbContext)
    {
        _orderCartsDbContext = orderCartsDbContext;
        _ordersDbContext = ordersDbContext;
    }

    public void Before(IMessageContext context)
    {
        if (context.Envelope?.Message is IQuery) return;

        _transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
    }

    public async Task After(IMessageContext context, CancellationToken cancellationToken)
    {
        if (context.Envelope?.Message is IQuery) return;

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