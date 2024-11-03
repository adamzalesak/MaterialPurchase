create table [dbo].[AccountingWarehouses]
(
    [Id]                            int                 not null,
    [Code]                          char(2)             not null,
    [Name]                          nvarchar(50)        not null,
    [IsActive]                      bit                 not null,
    [PhysicalWarehouseId]           int                 null
    
    constraint [PK_AccountingWarehouses] primary key clustered ([Id] asc)
)
