#region Namespace

using System;
using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Domain.Product.Command
{
    public class AddProductCommand : ICommand
    {
        public AddProductCommand()
        {
            Id = Guid.NewGuid();
        }

        public Entities.Product ProductDetails { get; set; }

        public Guid Id { get; set; }
    }
}