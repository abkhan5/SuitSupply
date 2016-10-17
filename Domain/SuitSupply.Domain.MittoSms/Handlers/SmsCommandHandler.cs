#region Namespace

using System;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.MittoSms.Command;

#endregion
namespace SuitSupply.Domain.MittoSms.Handlers
{
    public class SmsCommandHandler : ICommandHandler<AddNumberCommand>


    {
        public void Handle(ICommand command)
        {

            if (command.GetType() == typeof(AddNumberCommand))
            {
                Handle(command as AddNumberCommand);
            }

        }

        public void Handle(AddNumberCommand command)
        {

        }
    }
}
