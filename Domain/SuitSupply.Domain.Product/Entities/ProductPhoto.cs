using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuitSupply.Domain.Product.Entities
{
    [Table("SuitSupply.ProductPhotos")]
    public class ProductPhoto
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] ProductImage { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}