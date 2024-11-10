CREATE TABLE [orders].[OrderHeaders]
(
    [Id]          uniqueidentifier not null,
    [OrderCartId] uniqueidentifier not null,
    [Status]      int              not null,
    CONSTRAINT [PK_orders_OrderHeaders] primary key clustered ([Id] asc)
)
