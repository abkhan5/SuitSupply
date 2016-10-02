using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.Core.Messaging
{
    public interface ICommandHandlerRegistery
    {
        void Registery(ICommandHandler command);

        void ProcessCommand(ICommand command);
    }
}
