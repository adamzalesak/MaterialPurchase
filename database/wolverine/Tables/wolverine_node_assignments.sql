CREATE TABLE [wolverine].[wolverine_node_assignments]
(
    [id]         VARCHAR(100)        NOT NULL,
    [node_id]    UNIQUEIDENTIFIER    NULL,
    [started]    DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [PK_wolverine_node_assignments] PRIMARY KEY ([id]),
    CONSTRAINT [FK_wolverine_node_assignments_wolverine_nodes] FOREIGN KEY ([node_id]) REFERENCES [wolverine].[wolverine_nodes] ([id]),
);