using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Core.Extensions;
using SuitSupply.Domain.Product.Entities;
using SuitSupply.Domain.Product.ReadModel;

namespace SuitSupply.Domain.Product.Test
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        [DeploymentItem(@"Resources\car2.jpg")]
        [DeploymentItem(@"Resources\Echo.jpg")]
        [DeploymentItem(@"Resources\Kindle.jpg")]
        public void AddProductImageTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var product = ProductData.GetProduct();
            var pd = container.Resolve<IProductDao>();
            pd.AddProduct(product);
        }

        [TestMethod]
        public void AddProductTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var product = ProductData.GetProduct();
            var pd = container.Resolve<IProductDao>();
            pd.AddProduct(product);
            Assert.IsTrue(product.Id!=0);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var product = ProductData.GetProduct();
            var pd = container.Resolve<IProductDao>();
            pd.AddProduct(product);
            Assert.IsTrue(product.Id != 0);
            pd = container.Resolve<IProductDao>();
            var productId = product.Id;
            product = pd.GetProduct(product.Id);
            product.ProductName = "Romeo";
            pd.UpdateProduct(product);
        }

        [TestMethod]

        public void GetProductTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var pd = container.Resolve<IProductDao>();
            var products= pd.GetProducts().ToList();
            Assert.IsTrue(products!=null);
        }

    }

}
