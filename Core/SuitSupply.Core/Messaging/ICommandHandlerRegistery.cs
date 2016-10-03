namespace SuitSupply.Core.Messaging
{
    public interface ICommandHandlerRegistery
    {
        void Registery(ICommandHandler commandHandler);

        void ProcessCommand(ICommand command);
    }
}