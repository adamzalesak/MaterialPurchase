CREATE TABLE [wolverine].[wolverine_node_records]
(
    [id]             INT               NOT NULL IDENTITY,
    [node_number]    INT               NOT NULL,
    [event_name]     VARCHAR(100)      NOT NULL,
    [timestamp]      DATETIMEOFFSET    NOT NULL DEFAULT GETUTCDATE(),
    [description]    VARCHAR(500)      NULL,

    CONSTRAINT [PK_wolverine_node_records] PRIMARY KEY ([id])
);