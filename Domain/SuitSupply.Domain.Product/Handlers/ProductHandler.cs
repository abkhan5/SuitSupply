#region Namespace

using System;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product.Command;

#endregion

namespace SuitSupply.Domain.Product.Handlers
{
    public class ProductHandler :
        ICommandHandler<AddProductCommand>,
        ICommandHandler<AddProductsCommand>,
        ICommandHandler<UpdateProductCommand>,
        ICommandHandler<UpdateProductsCommand>
    {

        #region Private members

        private Lazy<IUnitOfWork> _dataAccess;
        private readonly Func<IUnitOfWork> _dataAccessInstanceMethod;
        private readonly Lazy<IUnitOfWork> _eventContextDal;

        #endregion

        #region Constructor

        public ProductHandler(Func<IUnitOfWork> dataAccessInstanceMethod, Func<IUnitOfWork> eventContextDal)
        {
            _dataAccessInstanceMethod = dataAccessInstanceMethod;
            _eventContextDal = new Lazy<IUnitOfWork>(eventContextDal);
            _dataAccess = new Lazy<IUnitOfWork>(_dataAccessInstanceMethod);
        }

        #endregion

        #region Public Methods

        public void Handle(AddProductCommand command)
        {
            var product = command.ProductDetails;

            _dataAccess.Value.AddEntity(product);
            _dataAccess.Value.Save();
        }

        public void Handle(ICommand command)
        {
            var commandResult = new Event
            {
                CommandId = command.Id.ToString(),
                Payload = command.GetType().Name,
                WasCommandSuccessfull = true
            };
            try
            {
                if (command.GetType() == typeof(AddProductCommand))
                    Handle(command as AddProductCommand);
                else if (command.GetType() == typeof(AddProductsCommand))
                    Handle(command as AddProductsCommand);
                else if (command.GetType() == typeof(UpdateProductsCommand))
                    Handle(command as UpdateProductsCommand);
                else if (command.GetType() == typeof(UpdateProductCommand))
                    Handle(command as UpdateProductCommand);
            }
            catch (Exception ex)
            {
                commandResult.WasCommandSuccessfull = false;
                throw;
            }
            finally
            {
                _dataAccess = new Lazy<IUnitOfWork>(_dataAccessInstanceMethod);

                _eventContextDal.Value.AddEntity(commandResult);
                _eventContextDal.Value.Save();
            }
        }

        public void Handle(AddProductsCommand command)
        {
            var products = command.ProductDetails;
            foreach (var product in products)
                _dataAccess.Value.AddEntity(product);
            _dataAccess.Value.Save();
        }

        public void Handle(UpdateProductCommand command)
        {
            var product = command.ProductDto;
            product.ProductPhotos.Clear();
            _dataAccess.Value.Update(product);
            _dataAccess.Value.Save();
        }

        public void Handle(UpdateProductsCommand command)
        {
            var products = command.ProductDetails;
            foreach (var product in products)
            {
                product.ProductPhotos.Clear();
                _dataAccess.Value.Update(product);
            }
            _dataAccess.Value.Save();
        }

        #endregion
    }
}