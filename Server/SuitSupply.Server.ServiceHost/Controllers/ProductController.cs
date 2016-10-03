#region Namespace

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
        public ProductController(ICommandBus bus, IProductDao dal)
        {
            _dal = dal;
            _bus = bus;
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


        // POST api/<controller>
        [HttpPost]
        //[ActionName("AddProduct")]
        public void Post(ProductDto productDto)
        {
            var product = productDto.ProductDtoToPoco();
            var command = new AddProductCommand() {ProductDetails = product};
            _bus.Send(command);
            //_dal.AddProduct(product);
        }

        
        
    }
}