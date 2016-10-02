namespace SuitSupply.Domain.Product.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuitSupply.ProductImages")]
    public partial class ProductImage
    {
        public int Id { get; set; }

        [Column("ProductImage", TypeName = "image")]
        [Required]
        public byte[] ProductImage1 { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Product Product { get; set; }
    }
}
