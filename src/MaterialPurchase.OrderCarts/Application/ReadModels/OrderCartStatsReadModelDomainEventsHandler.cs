using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.OrderCarts.Application.ReadModels;

public class OrderCartStatsReadModelDomainEventsHandler
{
    public static async Task<OrderCartStatsReadModel?> Load(IOrderCartReadModelRepository readModelRepository, CancellationToken cancellationToken)
    {
        return await readModelRepository.GetOrderCartStats(cancellationToken);
    }

    public static void Handle(OrderCartCreatedDomainEvent @event, OrderCartStatsReadModel? readModel, IOrderCartReadModelRepository readModelRepository)
    {
        if (readModel is not null)
        {
            readModel.Apply(@event);
            return;
        }

        readModel = new OrderCartStatsReadModel();
        readModel.Apply(@event);

        readModelRepository.Add(readModel);
    }

    public static void Handle(OrderCartFinishedDomainEvent @event, OrderCartStatsReadModel? readModel, IOrderCartReadModelRepository readModelRepository)
    {
        if (readModel is not null)
        {
            readModel.Apply(@event);
            return;
        }

        readModel = new OrderCartStatsReadModel();
        readModel.Apply(@event);

        readModelRepository.Add(readModel);
    }
}