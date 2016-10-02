using SuitSupply.Core.Extensions;

namespace SuitSupply.Domain.Product.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuitSupply.ProductProfile")]
    public partial class ProductProfile
    {
        public ProductProfile()
        {
            CreatedOn = DateTime.Now.ToUtcTime();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
