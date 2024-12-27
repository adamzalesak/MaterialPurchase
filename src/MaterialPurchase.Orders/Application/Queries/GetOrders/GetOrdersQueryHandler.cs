namespace MaterialPurchase.Orders.Application.Queries.GetOrders;

public static class GetOrdersQueryHandler
{
    public static Task<ICollection<GetOrdersResponse>> Handle(GetOrdersQuery query, IOrderReadRepository repository,
        CancellationToken cancellationToken)
    {
        return repository.GetOrders(cancellationToken);
    }
}