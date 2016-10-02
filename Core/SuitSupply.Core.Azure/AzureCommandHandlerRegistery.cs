using System.Collections.Generic;
using SuitSupply.Core.Messaging;

namespace SuitSupply.Core.Azure
{
   public  class AzureCommandHandlerRegistery: ICommandHandlerRegistery
   {
       private readonly Dictionary<string, ICommandHandler> _commandHandlers;
        public AzureCommandHandlerRegistery()
        {
            _commandHandlers= new Dictionary<string, ICommandHandler>();
        }
        public void Registery(ICommandHandler command) 
        {
            _commandHandlers[command.GetType().Name] = command ;
        }

       public void ProcessCommand(ICommand command)
       {
           var handler = _commandHandlers[command.GetType().Name];
           handler.Handle(command);
       }
   }
}
