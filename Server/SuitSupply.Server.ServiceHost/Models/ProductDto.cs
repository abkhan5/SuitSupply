using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuitSupply.Domain.Product.Entities;

namespace SuitSupply.Server.ServiceHost.Models
{
    public class ProductDto
    {
        public ProductDto(Product product)
        {
            this.Id = product.Id;
            this.CreatedOn = product.CreatedOn;
            var productProfile = product.ProductProfiles.OrderByDescending(item=>item.CreatedOn).First();

            this.ProductName = productProfile.ProductName;
            if (product.ProductImages==null)
            {
                return;
            }
            this.ProductImages = product.ProductImages.Select(item => item.ProductImage).ToList();
        }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string ProductName { get; set; }

        public IEnumerable<byte[]> ProductImages { get; set; }


    }
}