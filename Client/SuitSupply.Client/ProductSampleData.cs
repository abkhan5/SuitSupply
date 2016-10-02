using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Core.Extensions;
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
            var productImages= new List<byte[]>();
            productImages.Add(ImageExtensions.LoadImageToByte(null, "car2.jpg"));
            product.ProductImages = productImages;
            return product;
        }
    }
}
