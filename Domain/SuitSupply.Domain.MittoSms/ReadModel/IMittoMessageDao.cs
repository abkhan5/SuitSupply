using System.Collections.Generic;
using SuitSupply.DataContracts;

namespace SuitSupply.Domain.MittoSms.ReadModel
{
   public interface IMittoMessageDao
   {
       IEnumerable<CountryPackages> GetCountries();


        IEnumerable<Message> GetMessagesInRange(MessageSearchCriteria searchCriteria);

        IEnumerable<Statistic> GetStatisticsInRange(MessageSearchCriteria searchCriteria);
    }
}
