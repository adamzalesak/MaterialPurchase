# ADR 3: State Changes Through Events

## Date

2024-11-19

## Status

Accepted

## Context

The development team is tasked with implementing a material ordering system that requires auditability of domain actions
and efficient data retrieval. Event sourcing, which captures all state changes as a series of events, provides robust
auditability and flexibility. However, adopting full event sourcing introduces significant complexity, especially for a
team inexperienced with the pattern and accustomed to relational database-centric development. To address these
challenges while retaining the auditability benefits of event sourcing, a solution was designed to use domain events to
record state changes, inspired by event sourcing principles.

## Decision

The team decided to adopt a solution where aggregate state changes are consistently recorded through domain events. This
approach balances the benefits of event sourcing with the team's familiarity with relational database systems by
persisting both domain events and the current state of aggregates.

### Key Features

1. **State Changes Through Events**: Aggregates emit domain events to record state changes. These events trigger “Apply”
   methods, which update the aggregate state, ensuring all changes are captured consistently.
2. **Aggregate State Persistence**: The current state of each aggregate is stored in a relational database, enabling
   straightforward data retrieval using SQL queries.
3. **Event Store in SQL Server**: Domain events are stored alongside aggregate states in the same SQL Server database.
   Events and aggregate states are persisted within a single transaction, ensuring strong consistency.
4. **Asynchronous Projections**: To optimize read operations, domain events can be processed asynchronously to create
   denormalized read models tailored for specific use cases.

Example:

```csharp
// an aggregate method that triggers a domain event (only domain logic, no state changes)
public void Confirm()
{
    if (Status != OfferStatus.Draft)
    {
        throw new InvalidOperationException("Offer is not in draft status");
    }

    /* this method automatically triggers the Apply method below to update the aggregate state, 
       persists the event to the database, and publishes it through Wolverine */
    RaiseDomainEvent(new OfferConfirmedDomainEvent { OfferId = Id });
}

// an "Apply" method that updates the aggregate state based on a domain event
public void Apply(OfferConfirmedDomainEvent domainEvent)
{
    Status = OfferStatus.Confirmed;
}
```

This approach, inspired by event sourcing, provides a practical balance between auditability and implementation
feasibility while offering a pathway for the team to gradually adopt advanced event sourcing practices.

## Consequences

### Advantages

- **Enhanced Auditability**: The event store captures a complete history of domain actions, enabling retrospective
  analysis and ensuring accountability.
- **Familiar Query Patterns**: Aggregate states stored in the relational database can be queried directly using SQL,
  reducing the learning curve for the team.
- **Gradual Introduction of Event Sourcing Principles**: The approach introduces the team to event sourcing principles
  while maintaining compatibility with their current workflows. It lays the foundation for future adoption of advanced
  event sourcing features.
- **Optimized Performance When Needed**: Asynchronous projections can be implemented selectively to address performance
  bottlenecks in read operations.

### Limitations and Trade-offs

1. **State Duplication and Consistency Risk**: The aggregate state is stored both as domain events and as snapshots in
   the relational database. This introduces a risk of discrepancies if snapshots are updated without corresponding
   events. Strict adherence to design principles is required to mitigate this risk.
2. **Reduced Flexibility**: Full event sourcing capabilities, such as event replay and consistent projections, are not
   available. These features could be added later if needed.
3. **Custom Implementation Complexity**: Unlike using a library like Marten, this approach requires custom
   implementation, increasing development and maintenance effort.
4. **Event Versioning Challenges**: Breaking changes in events necessitate creating new event types and maintaining
   older ones, which can increase aggregate complexity over time.

## Alternatives Considered

1. **Full Event Sourcing**: While providing maximum auditability and flexibility, full event sourcing was deemed too
   complex for the team’s current experience level and would have required significant changes to their development
   approach.
2. **Traditional Database-Only Approach**: This approach would have aligned with the team’s familiarity but lacked
   robust auditability and would not support future scalability or domain-driven practices.

## Implementation Notes

- **Aggregate Direct State Changes Prohibited**: All state changes must be triggered by domain events to ensure
  consistency between the event store and aggregate states.
- **Aggregate History UI**: The event store allows for displaying aggregate histories. Although not currently requested
  by stakeholders, this feature can be implemented efficiently using the stored domain events.
- **Consistency Enforcement**: Transactions ensure consistency between event storage and aggregate state updates. Design
  guidelines will prohibit direct state modifications without corresponding domain events.
