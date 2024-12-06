create table [orderCarts].[DomainEvents]
(
    [Id]               uniqueidentifier not null,
    [SequenceNumber]   int              not null identity,
    [AggregateType]    nvarchar(100)    not null,
    [AggregateId]      uniqueidentifier not null,
    [AggregateVersion] uniqueidentifier not null,
    [OccurredOn]       datetimeoffset   not null,
    [Data]             nvarchar(max)    not null,
    [EventType]        nvarchar(200)    not null,
    constraint [PK_orderCarts_DomainEvents] primary key clustered ([Id])
)
GO

create nonclustered index [IX_orderCarts_DomainEvents_AggregateType_AggregateId]
    on [orderCarts].[DomainEvents] ([AggregateType], [AggregateId])