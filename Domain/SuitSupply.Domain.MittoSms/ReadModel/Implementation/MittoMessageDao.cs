using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataContracts;

namespace SuitSupply.Domain.MittoSms.ReadModel.Implementation
{
   internal  class MittoMessageDao: IMittoMessageDao
    {


        public IEnumerable<Country> GetCountries()
        {
            IEnumerable<Country> countries= new List<Country>();

            return countries;
        }


        public IEnumerable<ShortMessageService> GetMessagesInRange(MessageSearchCriteria searchCriteria)
        {
            IEnumerable<ShortMessageService> messages = new List<ShortMessageService>();

            return messages;


        }
    }
}
