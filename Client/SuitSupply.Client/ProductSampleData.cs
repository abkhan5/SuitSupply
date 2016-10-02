using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataObject;

namespace SuitSupply.Client
{
    class ProductSampleData
    {
        public static ProductDto GetSampleDto()
        {
            ProductDto product= new ProductDto();
            product.ProductName = "Cola";
            product.CreatedOn=DateTime.Now;
            product.ProductImages= new List<byte[]>();

            return product;
        }
    }
}
