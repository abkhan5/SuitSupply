using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuitSupply.Domain.Product.Entities;

namespace SuitSupply.Server.ServiceHost.Models
{
    public static class DtoToPocoTranslator
    {
        public static IEnumerable<ProductDto> ProductPocoToDto(this  IEnumerable<Product> froms)
        {
            foreach (var from in froms)
            {
                yield return ProductPocoToDto(from);
            }
        }

        public static ProductDto ProductPocoToDto(this Product from )
        {
            return new ProductDto(from);
        }

        public static Product ProductDtoToPoco(this ProductDto from)
        {
            var product= new Product();
            product.CreatedOn = from.CreatedOn;
            product.ProductProfiles.Add(new ProductProfile()
            {
                CreatedOn = from.CreatedOn,
                ProductName = from.ProductName
            });
            foreach (var fromProductImage in from.ProductImages)
            {
                var productPhoto=new ProductPhotos();
                productPhoto.CreatedOn = from.CreatedOn;
                productPhoto.ProductImage = fromProductImage;
                product.ProductImages.Add(productPhoto);
            }
            return product;
        }
    }
}