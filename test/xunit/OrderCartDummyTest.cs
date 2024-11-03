using FluentAssertions;
using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.OrderCarts.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Categories;

namespace XunitTests
{
    [Category("Coverage")]
    public class OrderCartDummyTest
    {
        public readonly IConfiguration configuration;

        public OrderCartDummyTest(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Fact]
        public void Create_ValidInput_Succeeds()
        {
            //Arrange
            var orderCartName = "dummy_order_cart";

            //Act
            var orderCart = OrderCart.Create(orderCartName);
            
            //Assert
            orderCart.Should().NotBeNull();
        }

        [Fact]
        public void Finish_ValidInput_Succeeds()
        {
            //Arrange
            var orderCart = new OrderCart(Guid.NewGuid(), "order_cart", OrderCartStatus.Processing);
            
            //Act
            orderCart.Finish();

            //Assert
            orderCart.Status.Should().Be(OrderCartStatus.Finished);
        }
    }
}