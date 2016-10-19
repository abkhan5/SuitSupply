﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataContracts;

namespace SuitSupply.Domain.MittoSms.ReadModel
{
   public interface IMittoMessageDao
   {
       IEnumerable<Country> GetCountries();


        IEnumerable<ShortMessageService> GetMessagesInRange(MessageSearchCriteria searchCriteria);
    }
}