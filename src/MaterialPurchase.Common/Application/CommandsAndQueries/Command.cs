namespace MaterialPurchase.Common.Application.CommandsAndQueries;

public interface ICommand<TResponse>;

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommand;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}