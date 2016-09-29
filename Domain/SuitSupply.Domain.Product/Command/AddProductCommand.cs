#region Namespace
using SuitSupply.Core.Messaging;
using System;

#endregion

namespace SuitSupply.Domain.Product.Command
{
    public class AddProductCommand : ICommand
    {
        public AddProductCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get;set;
        }

        public object ProductDto { get; set; }
    }
}
