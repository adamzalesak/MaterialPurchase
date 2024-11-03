CREATE TABLE [wolverine].[wolverine_nodes]
(
    [id]              UNIQUEIDENTIFIER    NOT NULL,
    [node_number]     INT                 NOT NULL IDENTITY,
    [description]     VARCHAR(100)        NOT NULL,
    [uri]             VARCHAR(100)        NOT NULL,
    [started]         DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [health_check]    DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [capabilities]    NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_wolverine_nodes] PRIMARY KEY ([id])
);