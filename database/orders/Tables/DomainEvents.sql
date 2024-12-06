create table [orders].[DomainEvents]
(
    [Id]            uniqueidentifier not null,
    [AggregateType] nvarchar(100)    not null,
    [AggregateId]   uniqueidentifier not null,
    [OccurredOn]    datetimeoffset   not null,
    [Data]          nvarchar(max)    not null,
    [EventType]     nvarchar(200)    not null,
    constraint [PK_orders_DomainEvents] primary key clustered ([Id])
);
