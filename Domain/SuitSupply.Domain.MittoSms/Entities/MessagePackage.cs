using System;

namespace SuitSupply.Domain.MittoSms.Entities
{
  public   class MessagePackage
    {
        public int ID { get; set; }

        public decimal PricePerSms{ get; set; }

        public string PackageName { get; set; }

        public int CountryId { get; set; }

        public DateTime ActiveTill { get; set; }
        
        public DateTime CreatedDateTime { get; set; }

    }
}
