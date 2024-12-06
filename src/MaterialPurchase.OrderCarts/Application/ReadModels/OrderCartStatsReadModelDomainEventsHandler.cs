using MaterialPurchase.OrderCartsContracts.DomainEvents;

namespace MaterialPurchase.OrderCarts.Application.ReadModels;

public class OrderCartStatsReadModelDomainEventsHandler
{
    public static async Task<OrderCartStatsReadModel?> Load(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        return await unitOfWork.OrderCartReadModels.GetOrderCartStats(cancellationToken) ?? new OrderCartStatsReadModel();
    }

    public static void Handle(OrderCartCreatedDomainEvent @event, OrderCartStatsReadModel? readModel, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (readModel is not null)
        {
            readModel.Apply(@event);
            return;
        }

        readModel = new OrderCartStatsReadModel();
        readModel.Apply(@event);

        unitOfWork.OrderCartReadModels.Add(readModel);
    }

    public static void Handle(OrderCartFinishedDomainEvent @event, OrderCartStatsReadModel? readModel, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (readModel is not null)
        {
            readModel.Apply(@event);
            return;
        }

        readModel = new OrderCartStatsReadModel();
        readModel.Apply(@event);

        unitOfWork.OrderCartReadModels.Add(readModel);
    }

    public static async Task After(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}