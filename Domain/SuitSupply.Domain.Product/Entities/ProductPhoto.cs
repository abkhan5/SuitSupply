namespace SuitSupply.Domain.Product.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuitSupply.ProductPhotos")]
    public partial class ProductPhoto
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] ProductImage { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
