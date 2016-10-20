using System;

namespace SuitSupply.DataContracts
{
    public class ShortMessageService
    {
        public int ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public DateTime SentDateTime { get; set; }
    }
}