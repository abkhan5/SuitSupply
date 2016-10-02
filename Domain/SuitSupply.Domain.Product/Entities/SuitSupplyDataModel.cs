namespace SuitSupply.Domain.Product.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SuitSupplyDataModel : DbContext
    {
        public SuitSupplyDataModel()
            : base("name=SuitSupplyDataModel")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductProfile> ProductProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductImages)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductProfiles)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
