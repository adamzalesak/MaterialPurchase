namespace MaterialPurchase.Orders.Application.Queries.GetOrder;

public static class GetOrderQueryHandler
{
    public static async Task<GetOrderResponse?> Handle(GetOrderQuery query, IOrderReadRepository orderReadRepository,
        CancellationToken cancellationToken)
    {
        var order = await orderReadRepository.GetByOrderCartId(query.OrderId, cancellationToken) ??
                    throw new AggregateException("Order not found");

        return new GetOrderResponse
        {
            OrderId = order.OrderId,
            Status = order.Status.ToString(),
            Items = order.Items.Select(x => new GetOrderOrderItemDto
            {
                OrderItemId = x.OrderItemId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Name = x.Name,
                Price = x.Price.Amount,
                Currency = x.Price.Currency,
            }),
        };
    }
}