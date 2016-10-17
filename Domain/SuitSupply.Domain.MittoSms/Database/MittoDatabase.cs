#region Namespace

using System.Data.Entity;
using SuitSupply.Core.DataAccess;

#endregion
namespace SuitSupply.Domain.MittoSms.Database
{
    
     public class MittoDatabase : DbContextBase
    {
        public MittoDatabase(string conn) : base(conn)
        {
        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Event));
        }
    }
}
