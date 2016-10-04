#region Namespace

using System;
using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Domain.Product.Command
{
    public class UpdateProductCommand : ICommand
    {
        public UpdateProductCommand()
        {
            Id = Guid.NewGuid();
        }

        public Entities.Product ProductDto { get; set; }

        public Guid Id { get; set; }
    }
}