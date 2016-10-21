#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using SuitSupply.Core;
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

        public MittoSmsController(ICommandBus bus, IMittoMessageDao dal, IEventDal eventDal)
        {
            _dal = dal;
            _bus = bus;
            _eventDal = eventDal;
        }



        [HttpPost]
        public MessageStateEnum SendSms(SmsRequest sms)
        {
            var countryCode = new string(sms.From.Take(3).ToArray());
            var country = _dal.GetCountries().First(cnItem => cnItem.CountryCode == countryCode);
            var message = new AddSmsCommand(sms);
            message.Message.CountryId = country.CountryID;
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
        public SentMessageResponse Get(MessageSearchCriteria criteria )
        {
            var messages = _dal.GetMessagesInRange(criteria).ToList();
            var response = new SentMessageResponse();// {Messages = messages, TotalMessages = messages.Count};
            return response;
        }

        [HttpGet]
        public SentMessageResponse Get(string criteria)
        {
          //var messages = _dal.GetMessagesInRange(criteria).ToList();
            var response = new SentMessageResponse();//{ Messages = messages, TotalMessages = messages.Count };
            return response;
        }

    }
}