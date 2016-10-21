CREATE TABLE [SuitSupply].[MessageState]
(
    [ID] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [SmsId] INT NOT NULL, 
    [MessageStatusId] DATETIME NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_MessageState_Messaging] FOREIGN KEY ([SmsId]) REFERENCES [SuitSupply].[ShortMessageService]([ID]), 

)
