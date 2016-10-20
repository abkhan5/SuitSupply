#region Namespace

using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Domain.MittoSms.ReadModel;

#endregion
namespace SuitSupply.Domain.MittoSms.Test
{
    [TestClass]
    public class DataaccessTest: MittoBaseClass
    {
        [TestMethod]
        public void GetCountryTest()
        {
            IMittoMessageDao dal = Container.Resolve<IMittoMessageDao>();
            var countries = dal.GetCountries().ToList();
            foreach (var country in countries)
            {
                Assert.IsNotNull(country);
                Assert.IsNotNull(country.MobileCountryCode);
                Assert.IsNotNull(country.PackageName);
                Assert.IsNotNull(country.PricePerSms);
                Assert.IsNotNull(country.CountryCode);
            }
        }
    }
}
