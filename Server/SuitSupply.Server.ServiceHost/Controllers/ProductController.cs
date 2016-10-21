#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SuitSupply.Core;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.Product.Command;
using SuitSupply.Domain.Product.ReadModel;
using SuitSupply.Server.ServiceHost.Translator;
using SuitSupply.DataContracts;
#endregion

namespace SuitSupply.Server.ServiceHost.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ICommandBus _bus;
        private readonly IUnityContainer _container;
        private readonly IProductDao _dal;
        private readonly IEventDal _eventDal;
        public ProductController(ICommandBus bus, IProductDao dal, IUnityContainer container, IEventDal eventDal)
        {
            _dal = dal;
            _bus = bus;
            _container = container;
            _eventDal = eventDal;
        }

        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            var products = _dal.GetProducts().ProductPocoToDto().ToList();
            return products;
        }

        [HttpGet]
        public ProductDto Get(int productCode)
        {
            var product = _dal.GetProduct(productCode).ProductPocoToDto();
            return product;
        }

        // GET api/<controller>/5


        // PUT api/<controller>
        [HttpPut]
        public void Put(ProductDto productDto)
        {
            var product = productDto.ProductDtoToPoco();
            var command = new UpdateProductCommand {ProductDto = product};
            _bus.Send(command);
            var commandResult = WaitUntilAvailable(command.ID);
            //if (commandResult)
            //{
            // throw   new Exception("Update failed");
            //}
        }

        // POST api/<controller>
        [HttpPost]
        public string Post(ProductDto productDto)
        {
            var product = productDto.ProductDtoToPoco();
            var command = new AddProductCommand {ProductDetails = product};
            _bus.Send(command);
            var commandResult = WaitUntilAvailable(command.ID);
            return commandResult ? "Success" : "Failed";}

        private bool WaitUntilAvailable(Guid commandId)
        {
            var result = _eventDal.WaitUntilAvailable(commandId);
            return result;
        }
    }
}