create table [orderCarts].[OrderCartHeaders]
(
    [Id]      uniqueidentifier not null,
    [Version] uniqueidentifier not null,
    [Name]    nvarchar(200)    not null,
    [Status]  int              not null,
    constraint [PK_orderCarts_OrderCartHeaders] primary key clustered ([Id] asc)
)
