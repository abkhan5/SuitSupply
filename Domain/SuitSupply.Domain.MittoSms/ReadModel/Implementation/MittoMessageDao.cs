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


        public IEnumerable<MessagingTransactions> GetMessagesInRange(MessageSearchCriteria searchCriteria)
        {
            var messages =_context.Query<MessagingTransactions>();
            var fromDate = searchCriteria.DateTimeFrom;
            if (fromDate.HasValue)
            {
                messages = messages.Where(msgItem => msgItem.SentDateTime >= fromDate);
            }
            var tillDate = searchCriteria.DateTimeTo;
            if (fromDate.HasValue)
            {
                messages = messages.Where(msgItem => msgItem.SentDateTime <= tillDate);
            }

            var recordsToSkip = searchCriteria.Skip;
            if (recordsToSkip.HasValue)
            {
                messages = messages.OrderBy(msgItem=>msgItem.SentDateTime).Skip(recordsToSkip.Value);
            }

            var recordsToTake = searchCriteria.Take;
            if (recordsToTake.HasValue)
            {
                messages = messages.Take(recordsToTake.Value);
            }
            var searchResult = messages.ToList();
            return searchResult;

        }
    }
}
