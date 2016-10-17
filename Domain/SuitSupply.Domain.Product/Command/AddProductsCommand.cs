#region Namespace

using System;
using System.Collections.Generic;
using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Domain.Product.Command
{
    public class AddProductsCommand : ICommand
    {
        public AddProductsCommand()
        {
            ID = Guid.NewGuid();
        }

        public IEnumerable<Entities.Product> ProductDetails { get; set; }

        public Guid ID { get; set; }
    }
}