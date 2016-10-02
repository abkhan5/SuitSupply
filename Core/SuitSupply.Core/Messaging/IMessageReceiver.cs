using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.Core.Messaging
{
    /// <summary>
    ///     Abstracts the behavior of a receiving component that raises
    ///     an event for every received event.
    /// </summary>
    public interface IMessageReceiver
    {
        /// <summary>
        /////     Starts the listener.
        ///// </summary>
        ///// <param name="messageHandler">Handler for incoming messages. The return value indicates how to release the message lock.</param>
        void Start();

        ///// <summary>
        /////     Stops the listener.
        ///// </summary>
        void Stop();
    }
}
