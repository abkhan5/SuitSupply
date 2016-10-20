using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace SuitSupply.Domain.MittoSms.Test
{
  public abstract  class MittoBaseClass
    {
        protected IUnityContainer Container { get; set; }

        protected MittoBaseClass()
        {
            var container = new UnityContainer();
            Container = container;
            Container.RegisterInstance(Container);
            Container.Resolve<MittoSmsDomain>();
        }
    }
}
