#region Namespace

using System.Collections.Generic;
using System.Linq;
using SuitSupply.DataContracts;
using SuitSupply.Domain.Product.Entities;

#endregion

namespace SuitSupply.Server.ServiceHost.Translator
{
    public static class DtoToPocoTranslator
    {
        public static IEnumerable<ProductDto> ProductPocoToDto(this IEnumerable<Product> froms)
        {
            foreach (var from in froms)
                yield return ProductPocoToDto(from);
        }

        public static ProductDto ProductPocoToDto(this Product from)
        {
            var product = new ProductDto();
            product.Id = from.Id;
            product.CreatedOn = from.CreatedOn;
            product.ProductCode = from.ProductCode;
            product.ProductName = from.ProductName;
            product.RecordVersion = from.RecordVersion;
            product.Id = from.Id;
            if (from.ProductPhotos != null)
                product.ProductImages = from.ProductPhotos.Select(item => item.ProductImage).ToList();
            return product;
        }

        public static Product ProductDtoToPoco(this ProductDto from)
        {
            var product = new Product();
            product.Id = from.Id;
            product.CreatedOn = from.CreatedOn;
            product.ProductName = from.ProductName;
            product.ProductCode = from.ProductCode;
            product.RecordVersion = from.RecordVersion;

            foreach (var fromProductImage in from.ProductImages)
            {
                var productPhoto = new ProductPhoto();
                productPhoto.CreatedOn = from.CreatedOn;
                productPhoto.ProductImage = fromProductImage;
                product.ProductPhotos.Add(productPhoto);
            }
            return product;
        }
    }
}