/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*						
--------------------------------------------------------------------------------------				
Post-Deployment Script - START
--------------------------------------------------------------------------------------
*/

	/*
	--------------------------------------------------------
	3.PostDeployment.StaticTablesList.sql - START
	--------------------------------------------------------
	*/
		PRINT '		3.PostDeployment.StaticTablesList.sql - START'
		GO

		:r ..\ContinuousDeployment\3.PostDeployment.StaticTablesList.sql
		GO

		PRINT '		3.PostDeployment.StaticTablesList.sql - END'
		GO

	/*
	--------------------------------------------------------
	3.PostDeployment.StaticTablesList.sql - END
	--------------------------------------------------------
	*/

	/*
	--------------------------------------------------------
	4.PostDeployment.Updates.sql - START
	--------------------------------------------------------
	*/
		PRINT '		4.PostDeployment.Updates.sql - START'
		GO

		:r ..\ContinuousDeployment\4.PostDeployment.Updates.sql
		GO

		PRINT '		4.PostDeployment.Updates.sql - END'
		GO
	/*
	--------------------------------------------------------
	4.PostDeployment.Updates.sql - END
	--------------------------------------------------------
	*/

	/*
	--------------------------------------------------------
	5.PostDeployment.Settings.sql - START
	--------------------------------------------------------
	*/
		PRINT '		5.PostDeployment.Settings.sql - START'
		GO

		:r ..\ContinuousDeployment\5.PostDeployment.Settings.sql
		GO

		PRINT '		5.PostDeployment.Settings.sql - END'
		GO
	/*
	--------------------------------------------------------
	5.PostDeployment.Settings.sql - END
	--------------------------------------------------------
	*/

	/*
	--------------------------------------------------------
	6.PostDeployment.Security.sql - START
	--------------------------------------------------------
	*/
		PRINT '		6.PostDeployment.Security.sql - START'
		GO

		:r ..\ContinuousDeployment\6.PostDeployment.Security.sql
		GO

		PRINT '		6.PostDeployment.Security.sql - END'
		GO
	/*
	--------------------------------------------------------
	6.PostDeployment.Settings.sql - END
	--------------------------------------------------------
	*/

	/*
	--------------------------------------------------------
	7.PostDeployment.Conventions.sql - START
	--------------------------------------------------------
	*/
		PRINT '		7.PostDeployment.Conventions.sql - START'
		GO

		:r ..\ContinuousDeployment\7.PostDeployment.Conventions.sql
		GO

		PRINT '		7.PostDeployment.Conventions.sql - END'
		GO
	/*
	--------------------------------------------------------
	7.PostDeployment.Conventions.sql - END
	--------------------------------------------------------
	*/




/*						
--------------------------------------------------------------------------------------				
Post-Deployment Script - END
--------------------------------------------------------------------------------------
*/
