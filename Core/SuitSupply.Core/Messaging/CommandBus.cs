#region Namespace

using System;
using System.Collections.Generic;

#endregion


namespace SuitSupply.Core.Messaging
{
    public class CommandBus : ICommandBus
    {

        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();



        public void Send(ICommand command)
        {
            
        }
    }
}
