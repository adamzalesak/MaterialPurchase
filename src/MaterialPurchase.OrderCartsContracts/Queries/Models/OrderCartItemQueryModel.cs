namespace MaterialPurchase.OrderCartsContracts.Queries.Models;

public class OrderCartItemQueryModel
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}