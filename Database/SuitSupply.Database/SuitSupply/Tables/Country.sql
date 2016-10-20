CREATE TABLE [SuitSupply].[Country]
(
    [ID] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [MobileCountryCode] NVARCHAR(50) NOT NULL, 
    [CountryCode] NVARCHAR(4) NOT NULL, 
    [CountryName] NVARCHAR(MAX) NOT NULL 
 
)
