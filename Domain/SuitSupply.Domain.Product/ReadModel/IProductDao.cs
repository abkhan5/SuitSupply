using System.Collections.Generic;

namespace SuitSupply.Domain.Product.ReadModel
{
    public interface IProductDao
    {
        IEnumerable<Entities.Product> GetProducts();
        Entities.Product GetProduct(int productId);
        void AddProduct(Entities.Product product);

        void UpdateProduct(Entities.Product product);
    }
}