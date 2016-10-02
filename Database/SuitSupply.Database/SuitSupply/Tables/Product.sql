CREATE TABLE [SuitSupply].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [CreatedOn] DATETIME NOT NULL, 
    [ProductCode] NVARCHAR(8) NOT NULL
)
