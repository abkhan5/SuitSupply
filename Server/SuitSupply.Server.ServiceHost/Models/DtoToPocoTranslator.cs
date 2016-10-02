using System.Collections.Generic;
using System.Linq;
using SuitSupply.Domain.Product.Entities;
using SuitSupply.DataObject;
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

        public static ProductDto ProductPocoToDto(this Product product)
        {
            ProductDto model = new ProductDto();
            model.Id = product.Id;
            model.CreatedOn = product.CreatedOn;
            var productProfile = product.ProductProfiles.OrderByDescending(item => item.CreatedOn).First();

            model.ProductName = productProfile.ProductName;
            if (product.ProductImages != null)
            {
                model.ProductImages = product.ProductImages.Select(item => item.ProductImage).ToList();
            }
            return model;
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