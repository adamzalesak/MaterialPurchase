using FluentAssertions;
using MaterialPurchase.Common.Domain.ValueObjects;
using MaterialPurchase.OrderCarts.Domain.Dtos;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.OrderCarts.Domain.OrderCart.Dtos;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using Xunit;

namespace UnitTests;

public class OrderCartTests
{
    [Fact]
    public void Create_ValidInput_Succeeds()
    {
        //Arrange
        var orderCartName = "Test Order Cart";

        //Act
        var orderCart = OrderCart.Create(orderCartName);

        //Assert
        orderCart.Should().NotBeNull();
        orderCart.Name.Should().Be(orderCartName);
        orderCart.Status.Should().Be(OrderCartStatus.Created);
        orderCart.Items.Should().BeEmpty();
        orderCart.DomainEvents.Should().HaveCount(1);
        orderCart.DomainEvents[0].Should().BeOfType<OrderCartCreatedDomainEvent>();
    }

    [Fact]
    public void Finish_ValidInput_Succeeds()
    {
        //Arrange
        var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Created);

        //Act
        orderCart.Finish();

        //Assert
        orderCart.Status.Should().Be(OrderCartStatus.Finished);
    }
    
    [Fact]
    public void Finish_AlreadyFinished_ThrowsException()
    {
        //Arrange
        var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Finished);

        //Act
        var act = () => orderCart.Finish();

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Order cart is already finished");
    }
    
    [Fact]
    public void OrderProduct_ValidInput_Succeeds()
    {
        //Arrange
        var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Created);
        var product = new ProductDto
        {
            Id = 1,
            Code = "product_code",
            Name = "product_name",
            Description = "product_description",
        };
        var offerId = Guid.NewGuid();
        var supplierId = 1;
        var quantity = 1;
        var price = new Money(1.0m, "EUR");
        
        var offerItem = new OfferItemDto
        {
            OfferId = offerId,
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            SupplierId = supplierId,
            AvailableQuantity = 10,
            Price = price,
        };

        //Act
        orderCart.OrderProduct(product, offerItem, quantity);

        //Assert
        orderCart.Items.Should().HaveCount(1);
        orderCart.Items.First().ProductId.Should().Be(product.Id);
        orderCart.Items.First().OfferId.Should().Be(offerId);
        orderCart.Items.First().SupplierId.Should().Be(supplierId);
        orderCart.Items.First().Quantity.Should().Be(quantity);
        orderCart.Items.First().Price.Should().Be(price);
        orderCart.DomainEvents.Should().HaveCount(1);
        orderCart.DomainEvents[0].Should().BeOfType<OrderCartItemOrderedDomainEvent>();
    }
    
    [Fact]
    public void OrderProduct_FinishedOrderCart_ThrowsException()
    {
        //Arrange
        var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Finished);
        var product = new ProductDto
        {
            Id = 1,
            Code = "product_code",
            Name = "product_name",
            Description = "product_description",
        };
        var offerId = Guid.NewGuid();
        var supplierId = 1;
        var quantity = 1;
        var price = new Money(1.0m, "EUR");
        
        var offerItem = new OfferItemDto
        {
            OfferId = offerId,
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            SupplierId = supplierId,
            AvailableQuantity = 10,
            Price = price,
        };

        //Act
        var act = () => orderCart.OrderProduct(product, offerItem, quantity);

        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot order product in finished order cart");
        orderCart.Items.Should().BeEmpty();
        orderCart.DomainEvents.Should().BeEmpty();
        orderCart.Status.Should().Be(OrderCartStatus.Finished);
    }
    
    [Fact]
    public void OrderProduct_OrderedQuantityChanged_Succeeds()
    {
        //Arrange
        var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Created);
        var product = new ProductDto
        {
            Id = 1,
            Code = "product_code",
            Name = "product_name",
            Description = "product_description",
        };
        var offerId = Guid.NewGuid();
        var supplierId = 1;
        var quantity = 1;
        var price = new Money(1.0m, "EUR");
        var offerItem = new OfferItemDto
        {
            OfferId = offerId,
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            SupplierId = supplierId,
            AvailableQuantity = 10,
            Price = price,
        };
        
        orderCart.OrderProduct(product, offerItem, quantity);

        //Act
        orderCart.OrderProduct(product, offerItem, quantity + 1);

        //Assert
        orderCart.Items.Should().HaveCount(1);
        orderCart.Items.First().Quantity.Should().Be(quantity + 1);
        orderCart.DomainEvents.Should().HaveCount(2);
        orderCart.DomainEvents[0].Should().BeOfType<OrderCartItemOrderedDomainEvent>();
        orderCart.DomainEvents[1].Should().BeOfType<OrderCartItemOrderedQuantityChangedDomainEvent>();
    }
}