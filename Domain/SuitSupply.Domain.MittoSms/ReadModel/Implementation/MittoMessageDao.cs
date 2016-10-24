#region Namespace

using System.Collections.Generic;
using System.Linq;
using SuitSupply.Core.DataAccess;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Database;
using SuitSupply.Domain.MittoSms.Entities;

#endregion
namespace SuitSupply.Domain.MittoSms.ReadModel.Implementation
{
   internal class MittoMessageDao: IMittoMessageDao
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
                    CountryID = country.ID,
                    CountryCode = country.CountryCode,
                    CountryName = country.CountryName,
                    PackageName = package.PackageName,
                    PricePerSms = package.PricePerSms,
                    MobileCountryCode = country.MobileCountryCode
                };

            return countries;
        }
        public  IEnumerable<Statistic> GetStatisticsInRange(MessageSearchCriteria searchCriteria)
       {
            var messages = from country in _context.Query<Country>()
                           join package in _context.Query<MessagePackage>()
                           on country.ID equals package.CountryId

                           join message in _context.Query<ShortMessageService>()
                           on country.ID equals message.CountryId

                           join messageState in _context.Query<MessageState>()
                           on message.ID equals messageState.SmsId

                           join messageStatus in _context.Query<MessageStatus>()
                           on messageState.MessageStatusId equals messageStatus.ID

                           select new Statistic
                           {
                               MobileCountryCode = country.MobileCountryCode,
                               SentDateTime = message.SentDateTime,
                               From = message.From,
                               To = message.To,
                               State = messageStatus.MessageStateCode=="Sent"?MessageStateEnum.Sent : MessageStateEnum.Failed,
                               Price = package.PricePerSms
                           };

        var fromDate = searchCriteria.DateTimeFrom;
            if (fromDate.HasValue)
            {
                messages = messages.Where(msgItem => msgItem.SentDateTime >= fromDate);
            }
    var tillDate = searchCriteria.DateTimeTo;
            if (tillDate.HasValue)
            {
                messages = messages.Where(msgItem => msgItem.SentDateTime <= tillDate);
            }
            

            var recordsToTake = searchCriteria.Take;
            if (recordsToTake.HasValue)
            {
                messages = messages.Take(recordsToTake.Value);
            }


            var searchResult = messages.ToList();
            return searchResult;

       }

        public IEnumerable<Message> GetMessagesInRange(MessageSearchCriteria searchCriteria)
        {
            var messages = from country in _context.Query<Country>()
                join message in _context.Query<ShortMessageService>()
                on country.ID equals message.CountryId

                join messageState in _context.Query<MessageState>()
                on message.ID equals  messageState.SmsId

                join messageStatus in _context.Query<MessageStatus>()
                on messageState.MessageStatusId equals messageStatus.ID
                select new Message
                {
                    CountryName = country.CountryName,
                    MobileCountryCode = country.MobileCountryCode,
                    CountryCode = country.CountryCode,
                    MessageStateCode = messageStatus.MessageStateCode,
                    MessgeStateDescription = messageStatus.MessgeStateDescription,
                    SentDateTime = message.SentDateTime,
                    From = message.From,
                    To = message.To,
                    Text = message.Text
                };

            var fromDate = searchCriteria.DateTimeFrom;
            if (fromDate.HasValue)
            {
                messages = messages.Where(msgItem => msgItem.SentDateTime >= fromDate);
            }
            var tillDate = searchCriteria.DateTimeTo;
            if (tillDate.HasValue)
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
