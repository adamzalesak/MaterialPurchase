using MaterialPurchase.OrderCartsContracts.DomainEvents;
using Wolverine;

namespace MaterialPurchase.Wolverine;

public static class WolverineLocalQueuesSetup
{
    public static WolverineOptions SetupLocalQueues(this WolverineOptions opts)
    {
        const string orderCartQueueName = "orderCart";
        const string orderQueueName = "order";

        // this setup ensures that all messages raised by the OrderCart aggregate are published to the local queue with strict ordering
        opts.Publish(c =>
        {
            c.MessagesImplementing<IOrderCartDomainEvent>();
            c.ToLocalQueue(orderCartQueueName).ListenWithStrictOrdering();
        });

        return opts;
    }
}