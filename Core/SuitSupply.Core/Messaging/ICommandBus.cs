
namespace SuitSupply.Core.Messaging
{
  public  interface ICommandBus
    {
        void Send(ICommand command);
    }
}
