CREATE PROCEDURE [wolverine].[uspDeleteIncomingEnvelopes]
    @IDLIST [wolverine].[EnvelopeIdList] READONLY
AS

DELETE
FROM [wolverine].[wolverine_incoming_envelopes]
WHERE [id] IN (SELECT [ID] FROM @IDLIST);