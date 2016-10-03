namespace SuitSupply.Core.Messaging
{
    /// <summary>
    ///     Abstracts the behavior of sending a message.
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        ///     Sends the specified message synchronously.
        /// </summary>
        // void Send(ICommand command,Action callBackOnSuccessFull, Action<Exception> callBackOnFail);
        void Send(ICommand command);
    }
}