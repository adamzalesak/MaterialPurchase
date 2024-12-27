using MaterialPurchase.Common.Domain.ValueObjects;

namespace MaterialPurchase.OrdersContracts.DomainEvents.Dtos;

public record OrderCreatedOrderItemDto
{
    public Guid OrderId { get; init; }
    public int ProductId { get; init; }
    public required string Name { get; init; }
    public int Quantity { get; init; }
    public required Money Price { get; init; }
}