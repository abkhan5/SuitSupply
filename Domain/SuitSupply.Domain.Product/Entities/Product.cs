using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SuitSupply.Core.Extensions;

namespace SuitSupply.Domain.Product.Entities
{
    [Table("SuitSupply.Product")]
    public class Product
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductPhotos = new HashSet<ProductPhoto>();
            CreatedOn = DateTime.Now.ToUtcTime();
        }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductCode { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RecordVersion { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}