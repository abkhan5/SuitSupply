#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Command;
using SuitSupply.Domain.MittoSms.ReadModel;

#endregion
namespace SuitSupply.Server.ServiceHost.Controllers
{
    public class MittoSmsController : ApiController
    {
        private readonly ICommandBus _bus;
        private readonly IEventDal _eventDal;
        private readonly IMittoMessageDao _dal;


        [InjectionConstructor]
        public MittoSmsController(ICommandBus bus, IMittoMessageDao dal, IEventDal eventDal)
        {
            _dal = dal;
            _bus = bus;
            _eventDal = eventDal;
        }



        [HttpPost]
        public MessageStateEnum SendSms(SmsRequest sms)
        {
            var message = new AddSmsCommand(sms);
            _bus.Send(message);
            var result = _eventDal.WaitUntilAvailable(message.ID);
            return result ? MessageStateEnum.Sent : MessageStateEnum.Failed;
        }


        [HttpGet]
        public IEnumerable<CountryPackages> GetCountries()
        {
            var countries = _dal.GetCountries().ToList();
            return countries;
        }


        
        [HttpGet]
        public SentMessageResponse GetSentSMS(
            DateTime dateTimeFrom,
            DateTime dateTimeTo,
            int skip, 
            int take)
        {
            MessageSearchCriteria criteria = new MessageSearchCriteria()
            {
                DateTimeFrom = dateTimeFrom,
                DateTimeTo = dateTimeTo,
                Skip = skip,
                Take = take
            };
            var messages = _dal.GetMessagesInRange(criteria).ToList();
            var response = new SentMessageResponse()
            {
                Messages = messages,
                TotalMessages = messages.Count
            };
            return response;
        }
        
        [HttpGet]
        public StatisticsResponse GetStatistics(DateTime dateTimeFrom,
            DateTime dateTimeTo,string mcc)
        {
            MessageSearchCriteria criteria = new MessageSearchCriteria()
            {
                DateTimeFrom = dateTimeFrom,
                DateTimeTo = dateTimeTo,
                Mcc = mcc
            };
            var messages = _dal.GetStatisticsInRange(criteria).ToList();
            var response = new StatisticsResponse()
            {
                Total= messages.Count ,
                Statistics = messages
            };
            return response;
        }

    }
}