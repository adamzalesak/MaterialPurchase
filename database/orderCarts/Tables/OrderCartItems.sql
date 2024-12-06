create table [orderCarts].[OrderCartItems]
(
    [Id]          uniqueidentifier not null,
    [OrderCartId] uniqueidentifier not null,
    [ProductId]   int              not null,
    [OfferId]     uniqueidentifier not null,
    [SupplierId]  int              not null,
    [Quantity]    int              not null,
    [Price]       money            not null,
    constraint [PK_orderCarts_OrderCartItems] primary key ([Id])
);