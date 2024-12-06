create table [orders].[DomainEvents]
(
    [Id]               uniqueidentifier not null,
    [SequenceNumber]   int              not null identity,
    [AggregateType]    nvarchar(100)    not null,
    [AggregateId]      uniqueidentifier not null,
    [AggregateVersion] uniqueidentifier not null,
    [OccurredOn]       datetimeoffset   not null,
    [Data]             nvarchar(max)    not null,
    [EventType]        nvarchar(200)    not null,
    constraint [PK_orders_DomainEvents] primary key clustered ([Id])
)
GO

create nonclustered index [IX_orders_DomainEvents_AggregateType_AggregateId]
    on [orders].[DomainEvents] ([AggregateType], [AggregateId])