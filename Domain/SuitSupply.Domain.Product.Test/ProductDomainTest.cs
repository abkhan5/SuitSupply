using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using SuitSupply.Domain.Product.ReadModel;

namespace SuitSupply.Domain.Product.Test
{
    [TestClass]
    public class ProductDomainTest
    {
        [TestMethod]
        public void RegistrationTest()
        {
            try
            {
                var container = new UnityContainer();
                container.RegisterInstance(container);
                container.Resolve<ProductDomain>();
                var pd = container.Resolve<IProductDao>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
