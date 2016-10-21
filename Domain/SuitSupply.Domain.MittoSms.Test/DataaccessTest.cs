#region Namespace

using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Core.Messaging;
using SuitSupply.DataContracts;
using SuitSupply.Domain.MittoSms.Command;
using SuitSupply.Domain.MittoSms.Handlers;
using SuitSupply.Domain.MittoSms.ReadModel;

#endregion
namespace SuitSupply.Domain.MittoSms.Test
{
    [TestClass]
    public class DataaccessTest: MittoBaseClass
    {

        [TestMethod]
        public void CommandHandlerTest()
        {
            var commandHandler = Container.Resolve<SmsCommandHandler>();
            var message = new SmsRequest()
            { From = "049985", To = "888", Text = "Hello"};
            ICommand command = new AddSmsCommand(message);
            commandHandler.Handle(command);
            Assert.IsNotNull(commandHandler);
        }
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
            var messageCriteris = new MessageSearchCriteria()
            {
                DateTimeFrom = DateTime.Now.AddDays(-5),
                DateTimeTo = DateTime.Now,
                Skip = 5,
                Take = 10

            };
            var result = dal.GetMessagesInRange(messageCriteris);
            Assert.IsNotNull(result);

        }
    }
}
