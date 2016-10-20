using System;

namespace SuitSupply.DataContracts
{
    public class MessagingTransactions
    {
        public int ID { get; set; }

        public int SmsId { get; set; }

        public DateTime SentDateTime { get; set; }

        public DateTime ReceivedDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}