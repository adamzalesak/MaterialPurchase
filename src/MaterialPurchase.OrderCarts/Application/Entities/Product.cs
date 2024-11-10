using System.ComponentModel.DataAnnotations;

namespace MaterialPurchase.OrderCarts.Application.Entities;

public record Product
{
    public int Id { get; init; }
    [Required, StringLength(50)] public required string Code { get; init; }
    [Required, StringLength(100)] public required string Name { get; init; }
    [StringLength(1000)] public required string Description { get; init; }
    public bool IsActive { get; init; }
}