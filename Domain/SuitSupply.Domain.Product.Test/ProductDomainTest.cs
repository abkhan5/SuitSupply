﻿using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Domain.Product.ReadModel;

namespace SuitSupply.Domain.Product.Test
{
    [TestClass]
    public class ProductDomainTest
    {
        [TestMethod]
        public void RegistrationTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var pd = container.Resolve<IProductDao>();
        }
    }
}