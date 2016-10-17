using System.Web.Http;
using Microsoft.Practices.Unity;
using SuitSupply.Core;
using SuitSupply.Core.Azure;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.MittoSms;
using SuitSupply.Domain.Product;
using Unity.WebApi;

namespace SuitSupply.Server.ServiceHost
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);
            container.RegisterType<IUnitOfWork, EventDbContext>
            (Constants.EventContextName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(DataAccessConstants.SuitConnectionString));
            RegisterDomains(container);
            RegisterBusComponents(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterDomains(IUnityContainer container)
        {
            container.Resolve<ProductDomain>();
            container.Resolve<MittoSmsDomain>();
        }

        private static void RegisterBusComponents(IUnityContainer container)
        {
            var commandBus = new AzureCommandBus(
                new TopicSender(
                    AzureConstants.SuitTopic, AzureConstants.TokenIssuer, AzureConstants.TokenAccessKey,
                    AzureConstants.ServiceUriScheme, AzureConstants.ServiceNamespace,
                    AzureConstants.ServicePath));
            container.RegisterInstance<ICommandBus>(commandBus);
        }
    }
}