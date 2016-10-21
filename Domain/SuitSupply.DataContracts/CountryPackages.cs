

namespace SuitSupply.DataContracts
{
  public   class CountryPackages
    {
        public int CountryID { get; set; }

        public string MobileCountryCode { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public decimal PricePerSms { get; set; }

        public string PackageName { get; set; }

    }
}
