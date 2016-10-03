#region Namespace

using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Core.Azure
{
    public class AzureCommandBus : ICommandBus
    {
        private readonly IMessageSender _messagSender;

        public AzureCommandBus(IMessageSender messagSender)
        {
            _messagSender = messagSender;
        }


        public void Send(ICommand command)
        {
            _messagSender.Send(command);
        }
    }
}