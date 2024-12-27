namespace MaterialPurchase.Offers.Application.Commands.CreateOffer;

public record CreateOfferRequest
{
    public int SupplierId { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string? Note { get; init; }
}