using System;

namespace SuitSupply.DataContracts
{
    public class Statistic
    {
        public DateTime SentDateTime { get; set; }
        public string MobileCountryCode { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Decimal Price { get; set; }
        public MessageStateEnum State { get; set; }
    }
}