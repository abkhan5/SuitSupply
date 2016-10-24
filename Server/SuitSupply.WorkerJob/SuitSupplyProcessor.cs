using System;
using Microsoft.Practices.Unity;
using SuitSupply.Core;
using SuitSupply.Core.Azure;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.MittoSms;
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
            container.Resolve<MittoSmsDomain>();

        }

        private void RegisterAzureComponent(IUnityContainer container)
        {
            container.RegisterType<ICommandHandlerRegistery, AzureCommandHandlerRegistery>(
                new ContainerControlledLifetimeManager());
            var handlerRegistery = container.Resolve<ICommandHandlerRegistery>();
            var prodHandlers = container.ResolveAll<ICommandHandler>();
            foreach (var prodHandler in prodHandlers)
                handlerRegistery.Registery(prodHandler);
            
            container.
                RegisterType<IMessageReceiver,SubscriptionReceiver>("ProductSubs",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(AzureConstants.SuitTopic, AzureConstants.AzureSubscribtions.AddTransaction,
                AzureConstants.TokenIssuer, AzureConstants.TokenAccessKey,
                AzureConstants.ServiceUriScheme, AzureConstants.ServiceNamespace,
                AzureConstants.ServicePath, handlerRegistery));
        }

        public void Start()
        {
            var subscribers = _container.ResolveAll<IMessageReceiver>();
            foreach (var subscriber in subscribers)
            {
                subscriber.Start();
            }

        }

        public void Stop()
        {
            var subscribers = _container.ResolveAll<IMessageReceiver>();
            foreach (var subscriber in subscribers)
            {
                subscriber.Stop();
            }
        }
    }
}