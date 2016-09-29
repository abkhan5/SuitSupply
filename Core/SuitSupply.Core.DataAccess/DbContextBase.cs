#region Namespace
using System;
using System.Collections.Generic;
using System.Data.Entity;
#endregion

namespace SuitSupply.Core.DataAccess
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        public DbContextBase(string connectionString):base(connectionString)
        {

        }

        protected abstract void OnModelCreation(DbModelBuilder modelBuilder);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            OnModelCreation(modelBuilder);
        }
        

        public void AddEntity<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                this.AddEntity(entity);
            }
        }

        public void AddEntity<T>(T entity) where T : class
        {
            this.Entry(entity);
        }


        public void Save()
        {
            this.SaveChanges();
        }

      
        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                this.Update(entity);
            }
        }

        public void Update<T>(T entity) where T : class
        {
            this.Entry(entity);
        }
    }
}
