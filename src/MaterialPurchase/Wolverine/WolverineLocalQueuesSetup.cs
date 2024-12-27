using MaterialPurchase.OffersContracts.DomainEvents;
using MaterialPurchase.OrderCartsContracts.DomainEvents;
using MaterialPurchase.OrdersContracts.DomainEvents;
using Wolverine;

namespace MaterialPurchase.Wolverine;

public static class WolverineLocalQueuesSetup
{
    const string OfferQueueName = "offer";
    const string OrderCartQueueName = "orderCart";
    const string OrderQueueName = "order";

    public static WolverineOptions SetupLocalQueues(this WolverineOptions opts)
    {
        // this setup ensures that all messages raised by the OrderCart aggregate are published to the local queue with strict ordering
        opts.Publish(c =>
        {
            c.MessagesImplementing<IOfferDomainEvent>();
            c.ToLocalQueue(OfferQueueName).ListenWithStrictOrdering();
        });

        opts.Publish(c =>
        {
            c.MessagesImplementing<IOrderCartDomainEvent>();
            c.ToLocalQueue(OrderCartQueueName).ListenWithStrictOrdering();
        });


        opts.Publish(c =>
        {
            c.MessagesImplementing<IOrderDomainEvent>();
            c.ToLocalQueue(OrderQueueName).ListenWithStrictOrdering();
        });

        return opts;
    }
}