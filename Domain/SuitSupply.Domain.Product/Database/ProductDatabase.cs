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
            
        }
    }
}
