CREATE PROCEDURE [wolverine].[uspDiscardAndReassignOutgoing]
    @DISCARDS [wolverine].[EnvelopeIdList] READONLY,
    @REASSIGNED [wolverine].[EnvelopeIdList] READONLY,
    @OWNERID INT

AS

DELETE
FROM [wolverine].[wolverine_outgoing_envelopes]
WHERE [id] IN (SELECT [ID] FROM @DISCARDS);

UPDATE [wolverine].[wolverine_outgoing_envelopes]
SET [owner_id] = @OWNERID
WHERE [ID] IN (SELECT [ID] FROM @REASSIGNED);