using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Registery(ICommandHandler commandHandler)
        {
            var genericHandler = typeof(ICommandHandler<>);
            var supportedCommandTypes = commandHandler.GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && (iface.GetGenericTypeDefinition() == genericHandler))
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();

            // Register this handler for each of he handled types.
            foreach (var commandType in supportedCommandTypes)
            {
                _commandHandlers.Add(commandType.Name, commandHandler);
            }
            
        }

       private string GetCommandName<T>(ICommandHandler<T> commandHandler) where T : ICommand
       {
           var commandType= typeof(T);
           var name = commandType.Name;
           return name;
       }

       public void ProcessCommand(ICommand command)
       {
           var name = command.GetType().Name;
           Console.WriteLine("Received command "+name);
           var handler = _commandHandlers[name];
           handler.Handle(command);
            Console.WriteLine("Command " + name+" handled");

        }
    }
}
