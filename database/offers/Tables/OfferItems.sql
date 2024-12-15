create table [offers].[OfferItems]
(
    [Id]                uniqueidentifier not null,
    [OfferId]           uniqueidentifier not null,
    [ProductId]         int              not null,
    [AvailableQuantity] int              null,
    [PriceAmount]       money            not null,
    [PriceCurrency]     nvarchar(3)      not null,
    constraint [PK_offers_OfferItems] primary key ([Id])
);