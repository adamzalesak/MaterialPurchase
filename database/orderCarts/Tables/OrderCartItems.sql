create table [orderCarts].[OrderCartItems]
(
    [Id]            uniqueidentifier not null,
    [Name]          nvarchar(200)    not null,
    [OrderCartId]   uniqueidentifier not null,
    [ProductId]     int              not null,
    [OfferId]       uniqueidentifier not null,
    [SupplierId]    int              not null,
    [Quantity]      int              not null,
    [PriceAmount]   money            not null,
    [PriceCurrency] nvarchar(3)      not null,
    constraint [PK_orderCarts_OrderCartItems] primary key ([Id])
);