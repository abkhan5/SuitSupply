using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.DataContracts
{
  public  class SentMessageResponse
    {
        public int TotalMessages { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
