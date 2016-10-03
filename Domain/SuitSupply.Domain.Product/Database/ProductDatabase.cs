#region Namespace
using SuitSupply.Core.DataAccess;
using System.Data.Entity;
#endregion

namespace SuitSupply.Domain.Product.Database
{
    public class ProductDatabase : DbContextBase
    {
        public ProductDatabase(string conn):base(conn)
        {

        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Event));
            modelBuilder.RegisterEntityType(typeof(Entities.Product));
            modelBuilder.RegisterEntityType(typeof(Entities.ProductPhoto));
            modelBuilder.Entity<Entities.ProductPhoto>().Property(p => p.ProductImage).HasColumnType("image");
        }
    }
}
