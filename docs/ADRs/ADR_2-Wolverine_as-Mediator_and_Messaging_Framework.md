# ADR 2: Wolverine as Mediator and Messaging Framework

## Date

2024-08-24

## Status

Proposed

## Context

The API endpoints generate commands and queries that need to be delegated to the application layer for processing. The
application layer is responsible for handling these commands and queries and returning the results to the API.

Additionally, the application publishes domain events, which are handled by its modules, as well as integration events
sent to Kafka for consumption by other services. It also subscribes to events from other services.

We need a tool that can handle these messaging patterns.

## Decision

We will use **Wolverine** for all mediation and messaging needs. Wolverine will be responsible for:

- Processing commands and queries from the API.
- Publishing and handling domain events.
- Sending/receiving integration events to/from Kafka.

## Consequences

### Flexibility in Messaging

Wolverine provides extensive configuration options and supports multiple transports, including RabbitMQ, Kafka, and
Azure Service Bus. It includes features for message resiliency, retries, and custom middleware, allowing for a highly
adaptable messaging solution.

### Built-in Outbox and Inbox Pattern

Wolverine includes built-in support for the outbox and inbox patterns. This ensures message durability and consistency,
preventing message loss during publication or consumption, which is critical for application reliability.

### Lower Ceremony Code

The authors of Wolverine prefer low-ceremony code that emphasizes simplicity and clarity. The framework is designed to
support this by reducing complexity and boilerplate code.

### Streamlined Toolset

By adopting Wolverine for both mediation and messaging, we consolidate our toolset, reducing the number of external
dependencies. This simplification helps ease the learning curve for developers and minimizes integration complexities.