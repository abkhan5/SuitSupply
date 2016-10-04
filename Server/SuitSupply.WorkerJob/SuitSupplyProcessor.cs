using System;
using Microsoft.Practices.Unity;
using SuitSupply.Core;
using SuitSupply.Core.Azure;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product;

namespace SuitSupply.WorkerJob
{
    internal class SuitSupplyProcessor : IDisposable
    {
        private readonly IUnityContainer _container;

        public SuitSupplyProcessor()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance(container);

            container.RegisterType<IUnitOfWork, EventDbContext>
            (Constants.EventContextName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(DataAccessConstants.SuitConnectionString));
            RegisterDomain(container);
            RegisterAzureComponent(container);
            _container = container;
        }

        public void Dispose()
        {
        }

        private void RegisterDomain(IUnityContainer container)
        {
            container.Resolve<ProductDomain>();
        }

        private void RegisterAzureComponent(IUnityContainer container)
        {
            container.RegisterType<ICommandHandlerRegistery, AzureCommandHandlerRegistery>(
                new ContainerControlledLifetimeManager());
            var handlerRegistery = container.Resolve<ICommandHandlerRegistery>();
            var prodHandlers = container.ResolveAll<ICommandHandler>();
            foreach (var prodHandler in prodHandlers)
                handlerRegistery.Registery(prodHandler);

            var subscriber = new SubscriptionReceiver(
                AzureConstants.Topic, AzureConstants.AddProductSub,
                AzureConstants.TokenIssuer, AzureConstants.TokenAccessKey,
                AzureConstants.ServiceUriScheme, AzureConstants.ServiceNamespace,
                AzureConstants.ServicePath, handlerRegistery);
            container.RegisterInstance(subscriber);
            //container.RegisterType<IMessageReceiver, SubscriptionReceiver>("AddProductSub");
            //var sub=container.Resolve<IMessageReceiver>("AddProductSub");
        }

        public void Start()
        {
            var subscriber = _container.Resolve<SubscriptionReceiver>();
            subscriber.Start();
        }

        public void Stop()
        {
            var subscriber = _container.Resolve<SubscriptionReceiver>();
            subscriber.Stop();
        }
    }
}