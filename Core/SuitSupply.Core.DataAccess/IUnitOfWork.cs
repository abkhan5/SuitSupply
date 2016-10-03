using System.Linq;

namespace SuitSupply.Core.DataAccess
{
    public interface IUnitOfWork
    {
        void AddEntity<T>(T entity) where T : class;

        IQueryable<T> Query<T>() where T : class;

        void Update<T>(T entity) where T : class;
        void Save();
    }
}