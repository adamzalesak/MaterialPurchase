CREATE PROCEDURE [wolverine].[uspMarkIncomingOwnership]
    @IDLIST [wolverine].[EnvelopeIdList] READONLY,
    @owner INT
AS

UPDATE [wolverine].[wolverine_incoming_envelopes]
SET [owner_id] = @owner, [status] = 'Incoming'
WHERE [id] IN (SELECT [ID] FROM @IDLIST);