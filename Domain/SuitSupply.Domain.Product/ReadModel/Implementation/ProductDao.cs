#region Namespace

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SuitSupply.Core.DataAccess;

#endregion

namespace SuitSupply.Domain.Product.ReadModel.Implementation
{
    public class ProductDao : IProductDao
    {
        private readonly IUnitOfWork _dataAccess;


        public ProductDao(IUnitOfWork dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<Entities.Product> GetProducts()
        {
            var products = ProductQuery();
            return products;
        }

        public Entities.Product GetProduct(int productId)
        {
            var product = ProductQuery().
                FirstOrDefault(item => item.Id == productId);
            return product;
        }

        public void AddProduct(Entities.Product product)
        {
            _dataAccess.AddEntity(product);
            _dataAccess.Save();
        }

        public void UpdateProduct(Entities.Product product)
        {
            _dataAccess.Update(product);
            _dataAccess.Save();
        }

        private IQueryable<Entities.Product> ProductQuery()
        {
            var products = _dataAccess.Query<Entities.Product>().
                Include(item => item.ProductPhotos);
            return products;
        }
    }
}