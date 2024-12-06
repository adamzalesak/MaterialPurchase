using System;
using FluentAssertions;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.Enums;
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
}