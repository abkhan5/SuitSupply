#region Namespace
using System;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product.Command;
using SuitSupply.Core.DataAccess;

#endregion
namespace SuitSupply.Domain.Product.Handlers
{
    public class ProductHandler :
        ICommandHandler<AddProductCommand>,
        ICommandHandler<AddProductsCommand>,
        ICommandHandler<UpdateProductCommand>,
        ICommandHandler<UpdateProductsCommand>
    {
        private Lazy<IUnitOfWork> _dataAccess;
        public ProductHandler(Func<IUnitOfWork> dataAccessInstanceMethod)
        {
            _dataAccess = new Lazy<IUnitOfWork>(dataAccessInstanceMethod);
        }

        public void Handle(UpdateProductsCommand command)
        {
            var products = command.ProductsDto;
            foreach (var product in products)
            {
                _dataAccess.Value.Update(product);
            }
            _dataAccess.Value.Save();
        }

        public void Handle(AddProductsCommand command)
        {
            var products = command.ProductsDto;
            foreach (var product in products)
            {
                _dataAccess.Value.AddEntity(product);
            }
            _dataAccess.Value.Save();
        }

        public void Handle(UpdateProductCommand command)
        {
            var product = command.ProductDto;
            _dataAccess.Value.Update(product);
        }

        public void Handle(AddProductCommand command)
        {
            var product = command.ProductDto;
            _dataAccess.Value.AddEntity(product);
        }
    }
}
