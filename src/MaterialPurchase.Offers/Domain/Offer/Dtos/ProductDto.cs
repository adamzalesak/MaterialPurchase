namespace MaterialPurchase.Offers.Domain.Offer.Dtos;

public record ProductDto
{
    public int Id { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public bool IsActive { get; init; }
}