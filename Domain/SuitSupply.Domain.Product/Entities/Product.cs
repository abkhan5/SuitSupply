using SuitSupply.Core.Extensions;

namespace SuitSupply.Domain.Product.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuitSupply.Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductPhotos = new HashSet<ProductPhoto>();
            CreatedOn=DateTime.Now.ToUtcTime();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
