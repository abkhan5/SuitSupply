#region Namespace

using System.Data.Entity;
using SuitSupply.Core.DataAccess;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Entities;

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
            modelBuilder.Entity<Country>().ToTable(TableNames.Country);
            modelBuilder.Entity<MessagePackage>().ToTable(TableNames.MessagePackage);
            modelBuilder.Entity<MittoSms.Entities.MessageStatus>().ToTable(TableNames.MessageStatus);
            modelBuilder.Entity<MessageState>().ToTable(TableNames.MessageState);
            modelBuilder.Entity<ShortMessageService>().ToTable(TableNames.ShortMessageService);
        }
    }
}
