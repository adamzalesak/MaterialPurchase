using MaterialPurchase.OrderCarts.Domain;
using MaterialPurchase.Orders.Domain.Order;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests;

public class ProjectReferencesTests
{
    [Fact]
    public void OrdersProjectDoesNotReferenceAnyOtherProjects()
    {
        var assembly = typeof(Order).Assembly;

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn("MaterialPurchase.OrderCarts")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void OrderCartsProjectDoesNotReferenceAnyOtherProjects()
    {
        var assembly = typeof(OrderCart).Assembly;

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn("MaterialPurchase.Orders")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}