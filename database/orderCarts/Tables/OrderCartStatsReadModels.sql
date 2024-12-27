CREATE TABLE [orderCarts].[OrderCartStatsReadModels]
(
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedCount]    INT              NOT NULL,
    [FinishedCount]   INT              NOT NULL,
    CONSTRAINT [PK_OrderCartStatsReadModels] PRIMARY KEY ([Id])
);
