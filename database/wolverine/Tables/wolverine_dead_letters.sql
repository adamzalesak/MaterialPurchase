CREATE TABLE [wolverine].[wolverine_dead_letters]
(
    [id]                   UNIQUEIDENTIFIER    NOT NULL,
    [execution_time]       DATETIMEOFFSET      NULL DEFAULT NULL,
    [body]                 VARBINARY(MAX)      NOT NULL,
    [message_type]         VARCHAR(250)        NOT NULL,
    [received_at]          VARCHAR(250)        NULL,
    [source]               VARCHAR(250)        NULL,
    [exception_type]       VARCHAR(MAX)        NULL,
    [exception_message]    VARCHAR(MAX)        NULL,
    [sent_at]              DATETIMEOFFSET      NULL,
    [replayable]           BIT                 NULL,

    CONSTRAINT [PK_wolverine_dead_letters_id] PRIMARY KEY ([id])
);