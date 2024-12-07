using MaterialPurchase.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public abstract class DbContextBase : DbContext
{
    public DbSet<DomainEventEnvelope> DomainEvents { get; set; }
    
    readonly IMessageBus _bus;

    protected DbContextBase(DbContextOptions options, IMessageBus bus)
        : base(options)
    {
        _bus = bus;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregateRoots = ChangeTracker
            .Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = aggregateRoots
            .SelectMany(e => e.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(e => e.ClearDomainEvents());
        
        var domainEventEnvelopes = domainEvents
            .Select(e => new DomainEventEnvelope(e))
            .ToList();

        DomainEvents.AddRange(domainEventEnvelopes);

        foreach (var domainEvent in domainEvents)
        {
            await _bus.PublishAsync(domainEvent);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}