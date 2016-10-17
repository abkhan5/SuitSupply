using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.ClientCommon;
using SuitSupply.DataContracts;

namespace SuitSupply.UWPClient.Repository
{
  public   class SuitSupplyRepository
  {
      private readonly SuitSupplyClient _client;
        public SuitSupplyRepository()
        {
            _client = new SuitSupplyClient();
        }

        public Task<IEnumerable<ProductDto>> GetProducs()
        {
          return  _client.GetProductsAsync();
        }
    }
}
