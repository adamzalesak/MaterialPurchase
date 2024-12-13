create table [offers].[OfferHeaders]
(
    [Id]         uniqueidentifier not null,
    [Version]    uniqueidentifier not null,
    [SupplierId] int              not null,
    [Status]     int              not null,
    [ValidFrom]  datetimeoffset   not null,
    [ValidTo]    datetimeoffset   null,
    [Note]       nvarchar(1000)   null,
    constraint [PK_offers_OfferHeaders] primary key clustered ([Id] asc)
)