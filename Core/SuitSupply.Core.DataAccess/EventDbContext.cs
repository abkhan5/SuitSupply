using System.Data.Entity;

namespace SuitSupply.Core.DataAccess
{
    public class EventDbContext : DbContextBase
    {
        public EventDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Event));
        }
    }
}