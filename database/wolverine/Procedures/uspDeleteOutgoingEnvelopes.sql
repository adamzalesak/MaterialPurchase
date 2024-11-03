CREATE PROCEDURE [wolverine].[uspDeleteOutgoingEnvelopes]
    @IDLIST [wolverine].[EnvelopeIdList] READONLY
AS

DELETE
FROM [wolverine].[wolverine_outgoing_envelopes]
WHERE [id] IN (SELECT [ID] FROM @IDLIST);