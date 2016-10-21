using System;

namespace SuitSupply.DataContracts
{
    public class Message
    {

        public string MobileCountryCode { get; set; }

        public string CountryCode { get; set; }


        public string MessageStateCode { get; set; }

        public string MessgeStateDescription { get; set; }
        
        public string CountryName { get; set; }

        public DateTime SentDateTime { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }


    }
}