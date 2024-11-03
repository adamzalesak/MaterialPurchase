CREATE TABLE [wolverine].[wolverine_incoming_envelopes]
(
    [id]                UNIQUEIDENTIFIER    NOT NULL,
    [status]            VARCHAR(25)         NOT NULL,
    [owner_id]          INT                 NOT NULL,
    [execution_time]    DATETIMEOFFSET      NULL DEFAULT NULL,
    [attempts]          INT                 NULL DEFAULT 0,
    [body]              VARBINARY(MAX)      NOT NULL,
    [message_type]      VARCHAR(250)        NOT NULL,
    [received_at]       VARCHAR(250)        NULL,
    [keep_until]        DATETIMEOFFSET      NULL,

    CONSTRAINT [PK_wolverine_incoming_envelopes] PRIMARY KEY ([id])
);