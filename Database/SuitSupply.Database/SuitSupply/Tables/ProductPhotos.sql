CREATE TABLE [SuitSupply].[ProductPhotos]
(
	[Id] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [ProductImage] IMAGE NOT NULL, 
    [ProductId] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    CONSTRAINT [FK_ProductPhoto_Product] FOREIGN KEY ([ProductId]) REFERENCES [SuitSupply].[Product]([ID])
)
