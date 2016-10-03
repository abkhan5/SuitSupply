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
            model.ProductCode = product.ProductCode;
            model.ProductName = product.ProductName;
            if (product.ProductPhotos != null)
            {
                model.ProductImages = product.ProductPhotos.Select(item => item.ProductImage).ToList();
            }
            return model;
        }
       
        public static Product ProductDtoToPoco(this ProductDto from)
        {
            var product= new Product();
            product.CreatedOn = from.CreatedOn;
            product.ProductName = from.ProductName;
            product.ProductCode = from.ProductCode;

            foreach (var fromProductImage in from.ProductImages)
            {
                var productPhoto=new ProductPhoto();
                productPhoto.CreatedOn = from.CreatedOn;
                productPhoto.ProductImage = fromProductImage;
                product.ProductPhotos.Add(productPhoto);
            }
            return product;
        }
    }
}