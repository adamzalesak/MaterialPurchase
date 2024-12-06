using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.OrderCarts.Application.ReadModels;

public class OrderCartStatsReadModel
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public int CreatedCount { get; set; }
    public int FinishedCount { get; set; }
    
    public void Apply(OrderCartCreatedDomainEvent @event)
    {
        CreatedCount++;
    }
    
    public void Apply(OrderCartFinishedDomainEvent @event)
    {
        CreatedCount--;
        FinishedCount++;
    }
}