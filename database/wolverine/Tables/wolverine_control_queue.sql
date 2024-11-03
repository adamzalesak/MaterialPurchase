CREATE TABLE [wolverine].[wolverine_control_queue]
(
    [id]              UNIQUEIDENTIFIER    NOT NULL,
    [message_type]    VARCHAR(100)        NOT NULL,
    [node_id]         UNIQUEIDENTIFIER    NOT NULL,
    [body]            VARBINARY(MAX)      NOT NULL,
    [posted]          DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [expires]         DATETIMEOFFSET      NULL,

    CONSTRAINT [PK_wolverine_control_queue] PRIMARY KEY ([id])
);