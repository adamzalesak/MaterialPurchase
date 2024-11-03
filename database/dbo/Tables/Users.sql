create table [dbo].[Users] (
    [Id]         int            identity (1, 1) not null,
    [FirstName]  nvarchar (100) null,
    [LastName]   nvarchar (100) null,
    [IsEnabled]  bit            not null,
    [ExternalId] nvarchar (450) null,
    [SId]        varchar (256)  null,
    [Email]      varchar (254)  null,
    [Login]      varchar (50)   null

    constraint [PK_Users] primary key clustered ([Id] asc)
)
