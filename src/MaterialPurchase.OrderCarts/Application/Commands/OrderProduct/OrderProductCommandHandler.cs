using MaterialPurchase.Common.Infrastructure.Persistence;
using MaterialPurchase.OffersContracts.ModuleQueries.GetActiveOfferItemsForProductId;
using MaterialPurchase.OrderCarts.Domain.Dtos;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Application.Commands.OrderProduct;

public static class OrderProductCommandHandler
{
    public static async Task<(OrderCart, ProductDto, ICollection<ActiveOfferItemForProductIdDto>)> Load(OrderProductCommand command,
        IAggregateRepository<OrderCart> repository,
        IProductReadRepository productReadRepository, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var orderCart = await repository.GetById(command.OrderCartId, cancellationToken) ??
                        throw new ArgumentException("Order cart not found");

        var products = await productReadRepository.GetProductsByIds(new List<int> { command.ProductId }, cancellationToken);
        if (products.Count == 0)
        {
            throw new ArgumentException("Product not found");
        }

        var product = products.First();

        var offerItemsQuery = new GetActiveOfferItemsForProductIdQuery(command.ProductId);
        var offerItemsResponse = await bus.InvokeAsync<GetActiveOfferItemsForProductIdResponse>(offerItemsQuery, cancellationToken);
        var offerItems = offerItemsResponse.OfferItems;

        return (orderCart, product, offerItems);
    }

    public static void Handle(OrderProductCommand command, OrderCart orderCart, ProductDto product,
        ICollection<ActiveOfferItemForProductIdDto> offerItems)
    {
        var offerItem = offerItems.FirstOrDefault(x => x.OfferId == command.OfferId) ??
                        throw new ArgumentException("Offer not found");

        orderCart.OrderProduct(
            product: product,
            offerItem: new OfferItemDto
            {
                OfferId = offerItem.OfferId,
                Id = offerItem.Id,
                Price = offerItem.Price,
                ProductId = offerItem.ProductId,
                SupplierId = offerItem.SupplierId,
                AvailableQuantity = offerItem.AvailableQuantity,
            },
            quantity: command.Quantity
        );
    }
}