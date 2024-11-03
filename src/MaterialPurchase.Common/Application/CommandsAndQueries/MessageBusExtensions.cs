using Wolverine;

namespace MaterialPurchase.Common.Application.CommandsAndQueries;

public static class MessageBusExtensions
{
    public static async Task<TResponse> InvokeCommandAsync<TResponse>(this IMessageBus bus, ICommand<TResponse> command,
        CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<TResponse>(command, cancellationToken);
    }

    public static async Task InvokeCommandAsync(this IMessageBus bus, ICommand command,
        CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(command, cancellationToken);
    }

    public static async Task<TResponse> InvokeQueryAsync<TResponse>(this IMessageBus bus, IQuery<TResponse> query,
        CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<TResponse>(query, cancellationToken);
    }
}