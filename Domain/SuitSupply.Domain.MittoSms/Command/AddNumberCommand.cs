using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Core.Messaging;

namespace SuitSupply.Domain.MittoSms.Command
{
   public class AddNumberCommand:ICommand
    {
        public AddNumberCommand()
        {
            ID=Guid.NewGuid();
        }
        public Guid ID { get; set; }

        public object Number { get; set; }
    }
}
