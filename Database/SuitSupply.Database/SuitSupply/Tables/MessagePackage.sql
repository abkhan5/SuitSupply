CREATE TABLE [SuitSupply].[MessagePackage]
(
  [ID] INT NOT NULL PRIMARY KEY  identity(1,1),  
  [PricePerSms] DECIMAL(10,4) NOT NULL,
  [PackageName] NVARCHAR(MAX) NOT NULL ,  
  [CountryId] INT NOT NULL,
  [CreatedOn] DATETIME NOT NULL, 
  [ActiveTill] DATETIME NOT NULL, 
    CONSTRAINT [FK_MessagingPackage_Country] FOREIGN KEY ([CountryId]) REFERENCES [SuitSupply].[Country]([ID])
  )
