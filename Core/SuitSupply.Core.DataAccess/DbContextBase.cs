#region Namespace

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

#endregion

namespace SuitSupply.Core.DataAccess
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        public DbContextBase(string connectionString) : base(connectionString)
        {
            this.Configuration.ProxyCreationEnabled = false;

        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }


        public void AddEntity<T>(T entity) where T : class
        {
            var entry = Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Added;
                Set<T>().Add(entity);
            }
        }


        public void Save()
        {
            SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        protected abstract void OnModelCreation(DbModelBuilder modelBuilder);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            OnModelCreation(modelBuilder);
        }
    }
}