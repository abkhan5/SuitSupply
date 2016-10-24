using System;
using System.Collections.Generic;
using System.Linq;
using SuitSupply.Core.Messaging;

namespace SuitSupply.Core.Azure
{
    public class AzureCommandHandlerRegistery : ICommandHandlerRegistery
    {
        private readonly Dictionary<string, ICommandHandler> _commandHandlers;

        public AzureCommandHandlerRegistery()
        {
            _commandHandlers = new Dictionary<string, ICommandHandler> ();
        }

        public void Registery(ICommandHandler commandHandler)
        {
            var genericHandler = typeof(ICommandHandler<>);
            var supportedCommandTypes = commandHandler
                .GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && (iface.GetGenericTypeDefinition() == genericHandler))
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();

            // Register this handler for each of he handled types.
            foreach (var commandType in supportedCommandTypes)
            {
                Console.WriteLine("Registering "+ commandType.Name+" to directory");
                _commandHandlers.Add(commandType.Name, commandHandler);
            }
        }

        public void ProcessCommand(ICommand command)
        {
            var name = command.GetType().Name;
            try
            {
                Console.WriteLine("Received command " + name);
                var handler = _commandHandlers[name];
                CastedHandler(handler, command);
                Console.WriteLine("Command " + name + " handled");
            }
            catch (Exception)
            {
                Console.WriteLine("Command " + name + " failed");
                throw;
            }
        }

        private string GetCommandName<T>(ICommandHandler<T> commandHandler) where T : ICommand
        {
            var commandType = typeof(T);
            var name = commandType.Name;
            return name;
        }

        private void CastedHandler<T>(ICommandHandler handler, T command) where T : ICommand
        {
            var castedHandler = handler as ICommandHandler<T>;
            if (castedHandler != null)
            {
                castedHandler.Handle(command);
            }
            else
            {
                lock (this)
                {
                    handler.Handle(command);

                }
            }
        }
    }
}