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
using SuitSupply.DataObject;
using SuitSupply.Domain.Product.Command;
using SuitSupply.Domain.Product.ReadModel;
using SuitSupply.Server.ServiceHost.Models;

#endregion
namespace SuitSupply.Server.ServiceHost.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductDao _dal;
        private readonly ICommandBus _bus;
        private readonly IUnityContainer _container;
        public ProductController(ICommandBus bus, IProductDao dal,IUnityContainer container)
        {
            _dal = dal;
            _bus = bus;
            _container = container;
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
            var command = new UpdateProductCommand() { ProductDto = product };
            _bus.Send(command);
            var commandResult = WaitUntilAvailable(command.Id.ToString());
            if (commandResult)
            {
             throw   new Exception("Update failed");
            }

        }

        // POST api/<controller>
        [HttpPost]
        public string Post(ProductDto productDto)
        {
            var product = productDto.ProductDtoToPoco();
            var command = new AddProductCommand() {ProductDetails = product};
            _bus.Send(command);
            var commandResult = WaitUntilAvailable(command.Id.ToString());
            return commandResult ? "Success" : "Failed";

        }
        private bool WaitUntilAvailable(string commandId)
        {
            var deadline = DateTime.Now.AddSeconds(Constants.WaitTimeoutInSeconds);
            var eventDal = _container.Resolve<IUnitOfWork>(Constants.EventContextName);
            
            while (DateTime.Now < deadline)
            {
                var eventResult = eventDal.Query<Event>().FirstOrDefault(item=>item.CommandId==commandId);

                if (eventResult != null)
                {
                    return eventResult.WasCommandSuccessfull;
                }

                Thread.Sleep(500);
            }

            return false;
        }


    }
}