#region Namespace
using System;
using System.Net.Cache;
using SuitSupply.Core.Messaging;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Entities;
using SuitSupply.Domain.MittoSms.Translator;

#endregion
namespace SuitSupply.Domain.MittoSms.Command
{
    public class AddSmsCommand :ICommand
    {
        public AddSmsCommand()
        {
            
        }
        public AddSmsCommand(SmsRequest message) : this(message.Translate())
        {
            
        }

        public AddSmsCommand(ShortMessageService message)
        {
            ID= Guid.NewGuid();
            Message = message;
        }

        public Guid ID { get; set; }

        public Entities.ShortMessageService Message { get; set; }
    }
}
