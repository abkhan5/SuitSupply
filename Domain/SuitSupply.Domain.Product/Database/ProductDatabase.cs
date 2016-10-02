#region Namespace
using SuitSupply.Core.DataAccess;
using System.Data.Entity;
#endregion

namespace SuitSupply.Domain.Product.Database
{
    public class ProductDatabase : DbContextBase
    {
        public ProductDatabase(string conn):base(conn)
            //:base(DataAccessConstants.SuitConnectionString)
        {

        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Entities.Product));
            modelBuilder.RegisterEntityType(typeof(Entities.ProductProfile));
            modelBuilder.RegisterEntityType(typeof(Entities.ProductPhotos));
            modelBuilder.Entity<Entities.ProductPhotos>().Property(p => p.ProductImage).HasColumnType("image");
        }
    }
}
