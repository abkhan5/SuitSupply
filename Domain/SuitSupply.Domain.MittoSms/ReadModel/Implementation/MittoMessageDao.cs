#region Namespace

using System.Collections.Generic;
using System.Linq;
using SuitSupply.Core.DataAccess;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Database;

#endregion
namespace SuitSupply.Domain.MittoSms.ReadModel.Implementation
{
   internal  class MittoMessageDao: IMittoMessageDao
   {
       private readonly IUnitOfWork _context;
        public MittoMessageDao(IUnitOfWork context)
        {
            _context= context;
        }
        public IEnumerable<CountryPackages> GetCountries()
        {
            var countries = from country in _context.Query<Country>()
                join package in _context.Query<MessagePackage>()
                on country.ID equals package.CountryId
                select new CountryPackages()
                {
                    CountryCode = country.CountryCode,
                    CountryName = country.CountryName,
                    PackageName = package.PackageName,
                    PricePerSms = package.PricePerSms,
                    MobileCountryCode = country.MobileCountryCode
                };

            return countries;
        }


        public IEnumerable<ShortMessageService> GetMessagesInRange(MessageSearchCriteria searchCriteria)
        {
            IEnumerable<ShortMessageService> messages = new List<ShortMessageService>();

            return messages;


        }
    }
}
