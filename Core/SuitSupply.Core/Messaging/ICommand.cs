#region Namespace

#endregion

using System;

namespace SuitSupply.Core.Messaging
{
    public interface ICommand
    {
        /// <summary>
        ///     Gets the command identifier.
        /// </summary>
        Guid ID { get; set; }
        

    }
}