using MaterialPurchase.Common.Domain;
using MaterialPurchase.OrdersContracts.DomainEvents.Dtos;

namespace MaterialPurchase.OrdersContracts.DomainEvents;

public record OrderCreatedDomainEvent : DomainEvent
{
    public override AggregateType AggregateType => AggregateType.Order;
    public Guid OrderCartId { get; init; }
    public int SupplierId { get; init; }
    public required ICollection<OrderCreatedOrderItemDto> Items { get; init; }
}