using MaterialPurchase.Common.Application.CommandsAndQueries;
using System.Transactions;
using Wolverine;

namespace MaterialPurchase.Wolverine;

public sealed class TransactionScopeMiddleware : IDisposable
{
    TransactionScope? _transactionScope;

    public void Before(IMessageContext context)
    {
        var messageInterfaces = context.Envelope?.Message?.GetType().GetInterfaces();
        var isQuery = messageInterfaces?.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>)) ?? false;

        if (isQuery)
        {
            return;
        }

        _transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
    }

    public void After()
    {
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