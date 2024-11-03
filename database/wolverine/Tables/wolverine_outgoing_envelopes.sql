CREATE TABLE [wolverine].[wolverine_outgoing_envelopes]
(
    [id]              UNIQUEIDENTIFIER    NOT NULL,
    [owner_id]        INT                 NOT NULL,
    [destination]     VARCHAR(250)        NOT NULL,
    [deliver_by]      DATETIMEOFFSET      NULL,
    [body]            VARBINARY(MAX)      NOT NULL,
    [attempts]        INT                 NULL DEFAULT 0,
    [message_type]    VARCHAR(250)        NOT NULL,

    CONSTRAINT [PK_wolverine_outgoing_envelopes] PRIMARY KEY ([id])
);