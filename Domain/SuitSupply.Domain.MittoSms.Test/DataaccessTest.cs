#region Namespace

using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.DataContracts;
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

        [TestMethod]
        public void GetMessagedTest()
        {
            IMittoMessageDao dal = Container.Resolve<IMittoMessageDao>();
            var messageCriteris= new MessageSearchCriteria()
            {
                DateTimeFrom = DateTime.Now.AddDays(-5),
                DateTimeTo =  DateTime.Now,
                Skip = 5,
                Take = 10

            };
            try
            {
                var result = dal.GetMessagesInRange(messageCriteris);
                Assert.IsNotNull(result);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
