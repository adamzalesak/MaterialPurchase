CREATE TABLE [orderCarts].[OrderCartHeaders]
(
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [Name]   NVARCHAR(200)    NOT NULL,
    [Status] INT              NOT NULL,
    CONSTRAINT [PK_orderCarts_OrderCartHeaders] PRIMARY KEY CLUSTERED ([Id] ASC)
)
