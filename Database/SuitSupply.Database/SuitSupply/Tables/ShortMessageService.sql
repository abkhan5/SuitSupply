CREATE TABLE [SuitSupply].[ShortMessageService]
(
[ID] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [From] NVARCHAR(MAX) NOT NULL, 
    [To] NVARCHAR(MAX) NOT NULL, 
    [Text] NVARCHAR(MAX) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
)
