using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public record OfferDto
{
    public Guid Id { get; init; }
    public int SupplierId { get; init; }
    public decimal Price { get; init; }
}

public class OrderProductCommandHandler
{
    public async Task<(OrderCart, OfferDto)> Load(OrderProductCommand command, IAggregateRepository<OrderCart> repository,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
               throw new ArgumentException("Order cart not found");
        var offer = new OfferDto
        {
            Id = command.OfferId,
            SupplierId = 1,
            Price = 100m
        };
        
        return (orderCart, offer);
    }

    public void Handle(OrderProductCommand command, OrderCart orderCart, OfferDto offer)
    {
        orderCart.OrderProduct(command.ProductId, command.OfferId, offer.SupplierId, command.Quantity, offer.Price);
    }
}