create table [orders].[OrderHeaders]
(
    [Id]          uniqueidentifier not null,
    [Version]     uniqueidentifier not null,
    [SupplierId]  int              not null,
    [OrderCartId] uniqueidentifier not null,
    [Status]      int              not null,
    CONSTRAINT [PK_orders_OrderHeaders] primary key clustered ([Id] asc)
)
