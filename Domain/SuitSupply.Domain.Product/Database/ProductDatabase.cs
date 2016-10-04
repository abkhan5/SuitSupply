#region Namespace

using System.Data.Entity;
using SuitSupply.Core.DataAccess;
using SuitSupply.Domain.Product.Entities;

#endregion

namespace SuitSupply.Domain.Product.Database
{
    public class ProductDatabase : DbContextBase
    {
        public ProductDatabase(string conn) : base(conn)
        {
        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Event));

            modelBuilder.RegisterEntityType(typeof(Entities.Product));
            modelBuilder.RegisterEntityType(typeof(ProductPhoto));
            modelBuilder.Entity<Entities.Product>().Property(s => s.RecordVersion).IsRowVersion();
            modelBuilder.Entity<Entities.Product>().Property(p => p.RecordVersion).IsConcurrencyToken();
            modelBuilder.Entity<ProductPhoto>().Property(p => p.ProductImage).HasColumnType("image");
        }
    }
}