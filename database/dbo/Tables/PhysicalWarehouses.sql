create table [dbo].[PhysicalWarehouses]
(
    [Id]            int                 not null,
    [Name]          nvarchar(100)       not null,
    [IsActive]      bit                 not null,
    [DcId]          int                 null,
    
    constraint [PK_PhysicalWarehouses] primary key clustered ([Id] asc)
)
