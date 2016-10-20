CREATE TABLE [SuitSupply].[MessagingTransactions]
(
     [ID] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [SmsId] INT NOT NULL, 
    [SentDateTime] DATETIME NOT NULL, 
    [ReceivedDateTime] DATETIME NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_MessagingTransactions_Messaging] FOREIGN KEY ([SmsId]) REFERENCES [SuitSupply].[ShortMessageService]([ID]), 

)
