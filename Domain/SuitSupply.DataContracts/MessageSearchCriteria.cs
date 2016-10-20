using System;

namespace SuitSupply.DataContracts
{
    public class MessageSearchCriteria
    {
        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}