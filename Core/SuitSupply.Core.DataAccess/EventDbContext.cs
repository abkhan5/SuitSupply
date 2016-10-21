#region Namespace
using System;
using System.Data.Entity;
using System.Threading;
using System.Linq;

#endregion

namespace SuitSupply.Core.DataAccess
{
    public class EventDbContext : DbContextBase, IEventDal
    {
        public EventDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Event));
        }
        
        public  bool WaitUntilAvailable(Guid commandID)
        {
            var deadline = DateTime.Now.AddSeconds(Constants.WaitTimeoutInSeconds);
            var commandId= commandID.ToString();
            while (DateTime.Now < deadline)
            {

                var eventResult = this.Query<Event>().FirstOrDefault(item => item.CommandId == commandId);

                if (eventResult != null)
                    return eventResult.WasCommandSuccessfull;

                Thread.Sleep(500);
            }

            return false;
        }

    }
}