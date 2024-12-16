create table [orders].[OrderItems]
(
    [Id]            uniqueidentifier not null,
    [Name]          nvarchar(200)    not null,
    [OrderId]       uniqueidentifier not null,
    [ProductId]     int              not null,
    [Quantity]      int              not null,
    [PriceAmount]   money            not null,
    [PriceCurrency] nvarchar(3)      not null,
    constraint [PK_orders_OrderItems] primary key ([Id])
);