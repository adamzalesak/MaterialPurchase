namespace MaterialPurchase.Common.Application.CommandsAndQueries;

public interface IQuery<TResponse>;

public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}

