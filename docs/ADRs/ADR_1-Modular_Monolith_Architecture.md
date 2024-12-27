# ADR 1: Modular Monolith Architecture

## Date

2024-08-22

## Status

Accepted

## Context

During the initial phases of application design, an Event Storming session identified several bounded contexts within
the system using pivotal events. To maximize modularity and maintain low coupling between these contexts, it's essential
to ensure these parts remain as isolated as possible. This isolation facilitates minimization of coupling and
potential partitioning of the application in the future.

Two architectural approaches are available for this division: **microservices** and a **modular monolith**. While
microservices offer flexibility and scalability, they also introduce significant complexity in managing distributed
systems, such as network latency and the overhead of maintaining multiple services. On the other hand, a modular
monolith can maintain simplicity but requires careful management to avoid tight coupling between modules over time.

## Decision

We will adopt a **modular monolith** architecture, where each module is represented by a separate project within a
single solution. Each module, corresponding to a potential bounded context, will have an associated Contracts project
that defines domain events, queries for inter-module communication, and data models. These Contracts projects can be
referenced by other modules as needed.

## Consequences

### Simplicity in Development

Managing, building, and deploying a single solution with multiple projects is simpler compared to handling
microservices.

### Simplified Testing and Debugging

Since the entire application runs as a single unit, testing and debugging are straightforward, avoiding the complexities
of distributed debugging required in microservices.

### Better Performance

The monolithic approach avoids the overhead of network calls and latency issues typical in microservices, leading to
better overall performance.

### Future-Proofing for Partitioning

The modular structure allows for future transition to microservices if necessary. This can be achieved by extracting a
module into a separate service and adjusting the dependencies in the Contracts project (converting queries to service
calls and domain events to integration events).

### Potential Risk of Coupling

There is a risk that over time, modules might become tightly coupled, leading to a loss of modularity. This should not
happen if we adhere to the rules of the architecture.

### Scalability Limitations

Scaling is limited to the monolithic application, which might be less flexible compared to the independent scaling of
microservices. However, the potential for future partitioning partially mitigates this concern.