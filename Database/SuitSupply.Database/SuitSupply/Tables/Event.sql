CREATE TABLE [SuitSupply].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
    [CommandId] NVARCHAR(100) NOT NULL, 
    [Payload] NVARCHAR(MAX) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [WasCommandSuccessfull] BIT NOT NULL
)
