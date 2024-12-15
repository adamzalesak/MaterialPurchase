using MaterialPurchase.Offers.Domain.Offer;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.Orders.Domain.Order;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests;

public class ProjectReferencesTests
{
    [Fact]
    public void OffersProjectDoesNotReferenceAnyOtherProjects()
    {
        var assembly = typeof(Offer).Assembly;

        var result = Types
            .InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn("MaterialPurchase.OrderCarts")
            .And()
            .NotHaveDependencyOn("MaterialPurchase.Orders")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void OrderCartsProjectDoesNotReferenceAnyOtherProjects()
    {
        var assembly = typeof(OrderCart).Assembly;

        var result = Types
            .InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn("MaterialPurchase.Offers")
            .And()
            .NotHaveDependencyOn("MaterialPurchase.Orders")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void OrdersProjectDoesNotReferenceAnyOtherProjects()
    {
        var assembly = typeof(Order).Assembly;

        var result = Types
            .InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn("MaterialPurchase.Offers")
            .And()
            .NotHaveDependencyOn("MaterialPurchase.OrderCarts")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}