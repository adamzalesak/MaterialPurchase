CREATE TABLE [continuousdeployment].[__MigrationLog]
(
	[SqlHash] BINARY(64) NOT NULL, 
    [DateStart] DATETIME2 NOT NULL, 
    [DateEnd] DATETIME2 NULL, 
    [SqlText] NVARCHAR(MAX) NOT NULL,
    [IsSuccessful] BIT NOT NULL DEFAULT 0, 
    [Error] NVARCHAR(MAX) NULL,
    [Author] NVARCHAR(250) NULL, 
    [Build] NVARCHAR(50) NULL,
	CONSTRAINT [PK_continuousdeployment___MigrationLog] PRIMARY KEY CLUSTERED ([SqlHash] ASC),
)