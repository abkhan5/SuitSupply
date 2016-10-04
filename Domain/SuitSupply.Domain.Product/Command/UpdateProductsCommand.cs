#region Namespace

using System;
using System.Collections.Generic;
using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Domain.Product.Command
{
    public class UpdateProductsCommand : ICommand
    {
        public UpdateProductsCommand()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<Entities.Product> ProductDetails { get; set; }

        public Guid Id { get; set; }
    }
}