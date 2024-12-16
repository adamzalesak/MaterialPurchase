# ADR 2: Wolverine as Mediator and Messaging Framework

## Date

2024-08-24

## Status

Accepted

## Context

The API endpoints generate commands and queries that need to be delegated to the handlers for processing. The
application layer is responsible for handling these commands and queries and returning the results to the API endpoint.

The application modules publish domain events, which are handled within the same module or different modules. Some
events may also be sent to Kafka for consumption by other systems. The application also subscribes to events from other
services.

Additionally, the system's modules sometimes need to communicate with each other synchronously. To achieve this, we need
a mediator that can route queries between modules.

Therefore, we need a tool that can handle the above requirements.

## Decision

We have chosen **Wolverine** as the framework for mediation and messaging.

- Processing commands and queries from the API endpoints.
- Publishing and handling domain events asynchronously.
- Sending/receiving integration events to/from Kafka.
- Mediating synchronous communication between application modules.

## Consequences

### Flexibility in Messaging

Wolverine provides extensive configuration options and supports multiple transports, including RabbitMQ, Kafka, and
Azure Service Bus. Its features for message resiliency, retries, and custom middleware make it a highly adaptable
messaging solution.

### Built-in Outbox and Inbox Pattern

Wolverine includes built-in support for the outbox and inbox patterns, ensuring message durability and preventing loss
during publication or consumption.

### Lower Ceremony Code

The authors of Wolverine emphasize low-ceremony code, which promotes simplicity and clarity. The framework supports this
philosophy by reducing complexity and boilerplate code.

### Streamlined Toolset

By adopting Wolverine for both mediation and messaging, we consolidate our toolset, reducing external dependencies. This
simplification minimizes integration complexities and lowers the learning curve for developers.