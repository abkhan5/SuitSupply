#region Namespace

#endregion
namespace SuitSupply.Core.Messaging
{
    public interface ICommandHandler
    {
        void Handle(ICommand command);
    }

    public interface ICommandHandler<T> : ICommandHandler
        where T : ICommand
    {
        void Handle(T command);
    }
}
