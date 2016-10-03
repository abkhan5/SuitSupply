using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.Domain.Product.ReadModel
{
   public interface  IProductDao
   {
       IEnumerable<Entities.Product> GetProducts();
       Entities.Product GetProduct(int productId);
       void AddProduct(Entities.Product product);
        
       void UpdateProduct(Entities.Product product);
    }
}
