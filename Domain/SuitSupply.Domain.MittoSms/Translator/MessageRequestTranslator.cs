using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Entities;

namespace SuitSupply.Domain.MittoSms.Translator
{
   public static  class MessageRequestTranslator
    {
  

        public static ShortMessageService Translate(this SmsRequest from)
        {
            ShortMessageService to = new ShortMessageService()
            {
                SentDateTime = DateTime.UtcNow,
                From = from.From,
                To = from.To,
                Text = from.Text,
                CreatedDateTime = DateTime.UtcNow
            };
            return to;
        }
    }
}
