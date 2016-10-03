using System.Data.Entity;
using Microsoft.Practices.Unity;
using SuitSupply.Core.DataAccess;
using SuitSupply.Domain.Product;
using SuitSupply.Domain.Product.Database;
using SuitSupply.Domain.Product.Entities;
using SuitSupply.Domain.Product.ReadModel;
using SuitSupply.Domain.Product.Test;

namespace SuitSupply.Core.DatabaseInitializer
{
    public class InitializeSuitSupply

    {
        public InitializeSuitSupply()
        {
            if (Database.Exists(DataAccessConstants.SuitConnectionString))
                return;


            Database.SetInitializer(new DropCreateDatabaseAlways<ProductDatabase>());

            IUnityContainer container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();

            var dal = container.Resolve<IProductDao>();
            dal.AddProduct(GetProduct());
        }


        private Product GetProduct()
        {
            return ProductData.GetProduct();
        }
    }
}