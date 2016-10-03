using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Core.Extensions;
using SuitSupply.DataObject;

namespace SuitSupply.Client
{
    static class ProductSampleData
    {
        public static ProductDto GetSampleDto()
        {
            Random random=new Random();
            ProductDto product= new ProductDto();
            product.ProductName = "Cola"+ random.Next(2);
            product.ProductCode = random.Next(99999);
            product.CreatedOn=DateTime.Now;
            var productImages= new List<byte[]>();
            productImages.Add(ImageExtensions.LoadImageToByte(null, "car2.jpg"));
            product.ProductImages = productImages;
            return product;
        }
    }
}
