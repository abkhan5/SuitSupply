using System;
using System.Collections.Generic;
using System.Text;

namespace SuitSupply.ClientCommon
{
    public static class ProductSampleData
    {
        public static ProductDto GetSampleDto()
        {
            var random = new Random();
            var product = new ProductDto();
            product.ProductName = "Cola" + random.Next(999);
            product.ProductCode = random.Next();
            product.CreatedOn = DateTime.Now;
            var productImages = new List<byte[]>();
            product.ProductImages = productImages;
            return product;
        }
    }

}
