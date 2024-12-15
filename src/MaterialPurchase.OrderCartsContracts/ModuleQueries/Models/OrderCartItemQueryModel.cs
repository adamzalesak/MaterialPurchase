namespace MaterialPurchase.OrderCartsContracts.ModuleQueries.Models;

public class OrderCartItemQueryModel
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}