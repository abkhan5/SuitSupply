#region Namespace

using System;
using Microsoft.Practices.Unity;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product.Database;
using SuitSupply.Domain.Product.Handlers;
using SuitSupply.Domain.Product.ReadModel;
using SuitSupply.Domain.Product.ReadModel.Implementation;

#endregion
namespace SuitSupply.Domain.Product
{
    public class ProductDomain
    {
        
        public ProductDomain(IUnityContainer container)
        {
            RegisterDatabase(container);
            RegisterCommand(container);
        }

        private void RegisterDatabase(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, ProductDatabase>
            (new TransientLifetimeManager(),
             new InjectionConstructor(DataAccessConstants.SuitConnectionString));

            container.RegisterType<IProductDao, ProductDao>();
        }

        private void RegisterCommand(IUnityContainer container)
        {
            container.RegisterType<ICommandHandler, ProductHandler>
                (ProductDomainConstant.ProductCommandHandler
                
                
                );
        }
    }
}
