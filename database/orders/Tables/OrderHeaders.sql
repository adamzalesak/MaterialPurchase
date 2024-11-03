CREATE TABLE [orders].[OrderHeaders]
(
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [OrderCartId] UNIQUEIDENTIFIER NOT NULL,
    [Status]      INT              NOT NULL,
    CONSTRAINT [PK_orders_OrderHeaders] PRIMARY KEY CLUSTERED ([Id] ASC)
)
