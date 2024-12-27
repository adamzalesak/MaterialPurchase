using MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;
using MaterialPurchase.OrderCarts.Domain.OrderCart;
using MaterialPurchase.OrderCarts.Infrastructure.Persistence;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests;

public class OrderCartsLayersTests
{
    [Fact]
    public void DomainDoesNotReferenceAnyOtherLayer()
    {
        var assembly = typeof(OrderCart).Assembly;

        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace("MaterialPurchase.OrderCarts.Domain")
            .ShouldNot()
            .HaveDependencyOnAny(
                "MaterialPurchase.OrderCarts.Application",
                "MaterialPurchase.OrderCarts.Api",
                "MaterialPurchase.OrderCarts.Infrastructure"
            )
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationDoesNotReferenceApiOrInfrastructure()
    {
        var assembly = typeof(GetOrderCartQuery).Assembly;

        var result = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespace("MaterialPurchase.OrderCarts.Application")
            .ShouldNot()
            .HaveDependencyOnAny(
                "MaterialPurchase.OrderCarts.Api",
                "MaterialPurchase.OrderCarts.Infrastructure"
            )
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void InfrastructureDoesNotReferenceApi()
    {
        var assembly = typeof(OrderCartReadRepository).Assembly;

        var result = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespace("MaterialPurchase.OrderCarts.Infrastructure")
            .ShouldNot()
            .HaveDependencyOn("MaterialPurchase.OrderCarts.Api")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}