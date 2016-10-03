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
        private readonly Lazy<IUnitOfWork> _dataAccess;

        private readonly Lazy<IUnitOfWork> _eventContextDal;
        public ProductHandler(Func<IUnitOfWork> dataAccessInstanceMethod,Func<IUnitOfWork> eventContextDal)
        {
            _eventContextDal = new Lazy<IUnitOfWork>(eventContextDal);
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
            var product = command.ProductDetails;
            _dataAccess.Value.AddEntity(product);
            _dataAccess.Value.Save();
        }

        public void Handle(ICommand command)
        {
            var commandResult= new Event();
            commandResult.CommandId= command.Id.ToString();
            commandResult.Payload = command.GetType().Name;
            commandResult.WasCommandSuccessfull = true;
            try
            {
                if (command.GetType() == typeof(AddProductCommand))
                {
                    Handle(command as AddProductCommand);
                }
                else if (command.GetType() == typeof(AddProductsCommand))
                {
                    Handle(command as AddProductsCommand);
                }
                else if (command.GetType() == typeof(UpdateProductsCommand))
                {
                    Handle(command as UpdateProductsCommand);
                }

                else if (command.GetType() == typeof(UpdateProductCommand))
                {
                    Handle(command as UpdateProductCommand);
                }
            }
            catch (Exception ex)
            {
                commandResult.WasCommandSuccessfull = false;
                throw;
            }
            finally
            {
                _eventContextDal.Value.AddEntity(commandResult);
                _eventContextDal.Value.Save();
            }
        }
    }
}
