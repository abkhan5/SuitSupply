CREATE TABLE [SuitSupply].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [CreatedOn] DATETIME NOT NULL, 
    [ProductCode] INT NOT NULL, 
    [RecordVersion] TIMESTAMP NOT NULL, 
    [ProductName] NVARCHAR(50) NOT NULL 
)
