create table [dbo].[Products]
(
    [Id]          int            not null,
    [Code]        nvarchar(50)   not null,
    [Name]        nvarchar(100)  not null,
    [Description] nvarchar(1000) null,
    [IsActive]    bit            not null,
    constraint [PK_Products] primary key clustered ([Id] asc)
)
