create table [offers].[OfferItems]
(
    [Id]                uniqueidentifier not null,
    [OfferId]           uniqueidentifier not null,
    [ProductId]         int              not null,
    [AvailableQuantity] int              not null,
    [Price]             money            not null,
    [Currency]          nvarchar(3)      not null,
    constraint [PK_offers_OfferItems] primary key ([Id])
);