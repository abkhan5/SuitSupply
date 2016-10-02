using Microsoft.Practices.Unity;
using System.Web.Http;
using SuitSupply.Core.Azure;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product;
using Unity.WebApi;

namespace SuitSupply.Server.ServiceHost
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.Resolve<ProductDomain>();
            RegisterBusComponents(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterBusComponents(IUnityContainer container)
        {
            var commandBus = new AzureCommandBus(
                        new TopicSender(
                            AzureConstants.Topic, AzureConstants.TokenIssuer, AzureConstants.TokenAccessKey,
                            AzureConstants.ServiceUriScheme, AzureConstants.ServiceNamespace,
                            AzureConstants.ServicePath));
            container.RegisterInstance<ICommandBus>(commandBus);
        }
    }
}