using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product.ReadModel;

namespace SuitSupply.Server.ServiceHost.Controllers
{
    public class MittoSmsController: ApiController
    {
        private readonly ICommandBus _bus;
        private readonly IUnityContainer _container;
        private readonly IProductDao _dal;

        public MittoSmsController(ICommandBus bus, IProductDao dal, IUnityContainer container)
        {
            _dal = dal;
            _bus = bus;
            _container = container;
        }

    }
}