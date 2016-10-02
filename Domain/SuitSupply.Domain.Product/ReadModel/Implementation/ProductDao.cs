#region Namespace
using SuitSupply.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

#endregion
namespace SuitSupply.Domain.Product.ReadModel.Implementation
{
    public class ProductDao : IProductDao
    {
        private IUnitOfWork  _dataAccess;


        public ProductDao( IUnitOfWork dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<Entities.Product> GetProducts()
        {
            var products = ProductQuery();
                return products;
        }

        private IQueryable<Entities.Product> ProductQuery()
        {
            var products = _dataAccess.Query<Entities.Product>().Include(item => item.ProductImages).Include(item => item.ProductProfiles);
            return products;
        }

        public Entities.Product GetProduct(int productCode)
        {
            var product = ProductQuery().
                FirstOrDefault(item=> item.ProductCode==productCode);
            return product;
        }

        public void AddProduct(Entities.Product product)
        {
            _dataAccess.AddEntity(product);
            _dataAccess.Save();
        }
    }
}
