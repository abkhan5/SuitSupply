#region Namespace

using Microsoft.Practices.Unity;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.MittoSms.Database;
using SuitSupply.Domain.MittoSms.Handlers;
using SuitSupply.Domain.MittoSms.ReadModel;
using SuitSupply.Domain.MittoSms.ReadModel.Implementation;

#endregion
namespace SuitSupply.Domain.MittoSms
{
    public class MittoSmsDomain
    {
        public MittoSmsDomain(IUnityContainer container)
        {
            RegisterDatabase(container);
            RegisterCommand(container);
        }

        private void RegisterDatabase(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, MittoDatabase>
            (new TransientLifetimeManager(),
                new InjectionConstructor(DataAccessConstants.SuitConnectionString));

            container.RegisterType<IMittoMessageDao, MittoMessageDao>();
        }

        private void RegisterCommand(IUnityContainer container)
        {
            container.RegisterType<ICommandHandler, SmsCommandHandler>
            (SmsCommandHandlers.SmsCommandHandler);
        }
    }
}
