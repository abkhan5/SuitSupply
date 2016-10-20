#region Namespace

using System.Data.Entity;
using SuitSupply.Core.DataAccess;
using SuitSupply.DataContracts;

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

           // modelBuilder.RegisterEntityType(typeof(MessagingTransactions));
            modelBuilder.Entity<MessagingTransactions>().ToTable(TableNames.MessagingTransactions);

           // modelBuilder.RegisterEntityType(typeof(ShortMessageService));
            modelBuilder.Entity<ShortMessageService>().ToTable(TableNames.ShortMessageService);

           // modelBuilder.RegisterEntityType(typeof(Country));
            modelBuilder.Entity<Country>().ToTable(TableNames.Country);


           // modelBuilder.RegisterEntityType(typeof(MessagePackage));
            modelBuilder.Entity<MessagePackage>().ToTable(TableNames.MessagePackage);
            
        }
    }
}
