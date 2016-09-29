#region Namespace
using SuitSupply.Core.Messaging;
using System;

using System.Collections.Generic;
#endregion


namespace SuitSupply.Domain.Product.Command
{
   public class AddProductsCommand : ICommand
    {
        public AddProductsCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get; set;
        }

        public IEnumerable<object> ProductsDto { get; set; }
    }
}
