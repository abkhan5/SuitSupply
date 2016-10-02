#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using SuitSupply.Core.Extensions;
using SuitSupply.Domain.Product.Entities;

#endregion
namespace SuitSupply.Domain.Product.Test
{
  public  class ProductData
    {

        public static Entities.Product GetProductWithImage()
        {
            var product = GetProduct();
            product.ProductImages = GetPhotos().ToList();

            return product;

        }

        public static Entities.Product GetProduct()
        {
            var random = new Random();
            var product = new Entities.Product()
            {
                ProductCode = random.Next(8),
                ProductProfiles = new List<ProductProfile>()
                {
                    new ProductProfile()
                    {
                        ProductName = random.Next(5).ToString()
                    }
                }
            };

            return product;
        }

        public static IEnumerable<Entities.ProductPhotos> GetPhotos()
        {
            var path = "car2.jpg";
            var productImage = new ProductPhotos();
            productImage.ProductImage = productImage.ProductImage.LoadImageToByte(path);
            yield return productImage;

            path = "Echo.jpg";
            productImage = new ProductPhotos();
            productImage.ProductImage = productImage.ProductImage.LoadImageToByte(path);
            yield return productImage;

            path = "Kindle.jpg";
            productImage = new ProductPhotos();
            productImage.ProductImage = productImage.ProductImage.LoadImageToByte(path);
            yield return productImage;
        }

    }
}
