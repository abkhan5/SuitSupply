#region Namespace
using SuitSupply.Core.DataAccess;
using System;

#endregion
namespace SuitSupply.Domain.Product.ReadModel.Implementation
{
    public class ProductDao : IProductDao
    {
        private Lazy<IUnitOfWork>  _dataAccess;

        public ProductDao( Func<IUnitOfWork> dataAccess)
        {
            _dataAccess = new Lazy<IUnitOfWork>(dataAccess);
        }
    }
}
