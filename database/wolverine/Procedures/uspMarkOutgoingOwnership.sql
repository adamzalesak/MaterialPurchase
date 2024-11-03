CREATE PROCEDURE [wolverine].[uspMarkOutgoingOwnership]
    @IDLIST [wolverine].[EnvelopeIdList] READONLY,
    @owner INT
AS

UPDATE [wolverine].[wolverine_outgoing_envelopes]
SET [owner_id] = @owner
WHERE [id] IN (SELECT [ID] FROM @IDLIST);