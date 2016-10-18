
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.DataContracts
{
  public  class Country
    {
        public int ID { get; set; }
        public string MobileCountryCode { get; set; }

        public string CountryCode { get; set; }

        public string Name { get; set; }

        public decimal PricePerSms { get; set; }
    }
}
