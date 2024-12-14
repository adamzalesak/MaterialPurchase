using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.Dtos;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

// TODO
public record OfferDto
{
    public Guid Id { get; init; }
    public int SupplierId { get; init; }
    public decimal Price { get; init; }
}

public static class OrderProductCommandHandler
{
    public static async Task<(OrderCart, OfferDto, ProductDto)> Load(OrderProductCommand command, IAggregateRepository<OrderCart> repository,
        IProductReadRepository productReadRepository,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
                        throw new ArgumentException("Order cart not found");
        var offer = new OfferDto
        {
            Id = command.OfferId,
            SupplierId = 1,
            Price = 100m,
        };

        var products = await productReadRepository.GetProductsByIds(new List<int> { command.ProductId }, cancellationToken);
        if (products.Count == 0)
        {
            throw new ArgumentException("Product not found");
        }

        var product = products.First();

        return (orderCart, offer, product);
    }

    public static void Handle(OrderProductCommand command, OrderCart orderCart, OfferDto offer, ProductDto product)
    {
        orderCart.OrderProduct(product, offer.Id, offer.SupplierId, command.Quantity, offer.Price);
    }
}