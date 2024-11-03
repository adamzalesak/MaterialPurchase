CREATE PROCEDURE [continuousdeployment].[SP_execute_script]
	@sql nvarchar(MAX),
	@author nvarchar(250),
	@build nvarchar(50) = NULL
AS
DECLARE @sqlHash binary(64) = HASHBYTES('SHA2_512', @sql);

IF NOT EXISTS (
		SELECT 1
FROM [ContinuousDeployment].[__MigrationLog]
WHERE [SqlHash] = @sqlHash
	AND [IsSuccessful] = 1
	)
	BEGIN
	BEGIN TRY
			IF NOT EXISTS (
				SELECT 1
	FROM [ContinuousDeployment].[__MigrationLog]
	WHERE [SqlHash] = @sqlHash
			)
			BEGIN
		INSERT INTO [ContinuousDeployment].[__MigrationLog]
			([SqlHash], [DateStart], [SqlText], [Author], [Build])
		VALUES(@sqlHash, SYSUTCDATETIME(), @sql, @author, @build)
	END
			ELSE BEGIN
		UPDATE [ContinuousDeployment].[__MigrationLog]
				SET [DateStart] = SYSUTCDATETIME(),
					--not so likely to change but just in case..
					[SqlText] = @sql,
					[Author] = @author,
					[Build] = @build
				WHERE [SqlHash] = @sqlHash
	END
			
			BEGIN TRANSACTION
				PRINT 'Executing ' + CONVERT(varchar(max), @sqlHash, 1) + 
					': ' + LEFT(@sql, 15) + '...';

				EXECUTE sp_executesql @sql;
			
				UPDATE [ContinuousDeployment].[__MigrationLog]
				SET [DateEnd] = SYSUTCDATETIME(),
					[Error] = NULL,
					[IsSuccessful] = 1
				WHERE [SqlHash] = @sqlHash

			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;

			UPDATE [ContinuousDeployment].[__MigrationLog]
			SET [DateEnd] = SYSUTCDATETIME(),
				[IsSuccessful] = 0,
				[Error] = FORMATMESSAGE('%d: %s. ', ERROR_NUMBER(), ERROR_MESSAGE())
			WHERE [SqlHash] = @sqlHash; 
			-- throw again
			THROW
		END CATCH
END
