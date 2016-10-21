using System;

namespace SuitSupply.Domain.MittoSms.Entities
{
    public class MessageState
    {
        public int ID { get; set; }

        public int SmsId { get; set; }
        public int MessageStatusId { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}