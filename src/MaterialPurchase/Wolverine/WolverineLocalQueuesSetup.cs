using MaterialPurchase.OrderCartsContracts.DomainEvents;
using Wolverine;

namespace MaterialPurchase.Wolverine;

public static class WolverineLocalQueuesSetup
{
    const string OrderCartQueueName = "orderCart";
    const string OrderQueueName = "order";

    public static WolverineOptions SetupLocalQueues(this WolverineOptions opts)
    {
        // this setup ensures that all messages raised by the OrderCart aggregate are published to the local queue with strict ordering
        opts.Publish(c =>
        {
            c.MessagesImplementing<IOrderCartDomainEvent>();
            c.ToLocalQueue(OrderCartQueueName).ListenWithStrictOrdering();
        });

        return opts;
    }
}