using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.DataContracts
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
