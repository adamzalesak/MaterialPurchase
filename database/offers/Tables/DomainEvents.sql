create table [offers].[DomainEvents]
(
    [Id]               uniqueidentifier not null,
    [SequenceNumber]   int              not null identity,
    [AggregateType]    nvarchar(100)    not null,
    [AggregateId]      uniqueidentifier not null,
    [AggregateVersion] uniqueidentifier not null,
    [OccurredOn]       datetimeoffset   not null,
    [Data]             nvarchar(max)    not null,
    [EventType]        nvarchar(200)    not null,
    constraint [offers_DomainEvents] primary key clustered ([Id])
)
GO

create nonclustered index [IX_offers_DomainEvents_AggregateType_AggregateId]
    on [offers].[DomainEvents] ([AggregateType], [AggregateId])