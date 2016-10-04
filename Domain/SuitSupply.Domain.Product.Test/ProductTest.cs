using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsTrue(product.Id != 0);
        }


        private IUnityContainer InitializeContainer()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            return container;
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            var container = InitializeContainer();
            var oldProduct = ProductData.GetProduct();
            var dal = container.Resolve<IProductDao>();
            dal.AddProduct(oldProduct);
            Assert.IsTrue(oldProduct.Id != 0);

            dal = container.Resolve<IProductDao>();
            var product = dal.GetProduct(oldProduct.Id);
            product.ProductName = "Romeo";

            dal = container.Resolve<IProductDao>();
            dal.UpdateProduct(product);

            product.ProductName = "Rome";
            dal.UpdateProduct(product);

            container = InitializeContainer();

            dal = container.Resolve<IProductDao>();
            product = dal.GetProduct(oldProduct.Id);

            Assert.AreNotEqual(oldProduct.ProductName, product.ProductName);
        }


        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void UpdateProductFailedTest()
        {
            var container = InitializeContainer();
            var oldProduct = ProductData.GetProduct();
            var dal = container.Resolve<IProductDao>();
            dal.AddProduct(oldProduct);
            Assert.IsTrue(oldProduct.Id != 0);
            dal = container.Resolve<IProductDao>();
            var product = dal.GetProduct(oldProduct.Id);
            product.ProductName = "Rome";
            dal.UpdateProduct(product);
            Assert.AreNotEqual(oldProduct.ProductName, product.ProductName);

            container = InitializeContainer();

            dal = container.Resolve<IProductDao>();
            dal.UpdateProduct(oldProduct);
        }

        [TestMethod]
        public void GetProductTest()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.Resolve<ProductDomain>();
            var pd = container.Resolve<IProductDao>();
            var products = pd.GetProducts().ToList();
            Assert.IsTrue(products != null);
        }
    }
}