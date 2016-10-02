CREATE TABLE [SuitSupply].[ProductProfile]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [ProductName] NVARCHAR(50) NOT NULL, 
    [ProductId] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    CONSTRAINT [FK_ProductProfile_Product] FOREIGN KEY ([ProductId]) REFERENCES [SuitSupply].[Product]([ID])
)
