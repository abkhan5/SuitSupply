using Microsoft.Practices.Unity;
using SuitSupply.Core.DataAccess;

namespace SuitSupply.Domain.MittoSms.Test
{
  public abstract  class MittoBaseClass
    {
        protected IUnityContainer Container { get; set; }

        protected MittoBaseClass()
        {
            var container = new UnityContainer();
            Container = container;
            Container.RegisterInstance(container);
            container.RegisterType<IEventDal, EventDbContext>
           (new ContainerControlledLifetimeManager(),
            new InjectionConstructor(DataAccessConstants.SuitConnectionString));
            Container.Resolve<MittoSmsDomain>();
        }
    }
}
