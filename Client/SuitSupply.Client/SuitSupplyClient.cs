#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataContracts;

#endregion
namespace SuitSupply.ClientCommon
{
    
   public class SuitSupplyClient
    {
        private const string LocalServicePath = "http://localhost/SuitSupply.Server.ServiceHost/";
        private const string AzureServicePath = "http://appversewebapi.azurewebsites.net/";

        public const string ServicePath = AzureServicePath;

        private HttpClient _client;

        public SuitSupplyClient()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServicePath) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Post

        public async Task PostProduct()
        {
            var product = ProductSampleData.GetSampleDto();
            await CreateProductAsync(product);
        }

        private async Task<Uri> CreateProductAsync(ProductDto product)
        {
            var response = await _client.PostAsJsonAsync("api/product", product);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
                Console.WriteLine(" Product successfully added");
            else
                Console.WriteLine(" Product add failed");
            // return URI of the created resource.
            return response.Headers.Location;
        }

        #endregion

        #region Put

        public async Task UpdateProduct()
        {
            var getProductTask = GetProductsAsync();
            getProductTask.Wait();
            var products = getProductTask.Result.ToList();
            if (!products.Any())
            {
                Console.WriteLine("No product to update");
            }
            else
            {
                var random = new Random();
                var product = products.First();
                product.ProductName = "Pepsi" + random.Next();
                Console.WriteLine("Product to be updated to " + product);
                InitializeClient();
                await UpdateProductAsync(product);
            }
        }

        private async Task<Uri> UpdateProductAsync(ProductDto product)
        {
            var response = await _client.PutAsJsonAsync("api/product", product);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
                if (response.IsSuccessStatusCode)
                    Console.WriteLine(" Product successfully updated");
                else
                    Console.WriteLine(" Product update failed");
            // return URI of the created resource.
            return response.Headers.Location;
        }

        #endregion

        #region Get Product

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            IEnumerable<ProductDto> products = new List<ProductDto>();
            using (_client)
            {
                var res = await _client.GetAsync("api/product");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    products = await res.Content.ReadAsAsync<IEnumerable<ProductDto>>();
                    foreach (var product in products)
                        Console.WriteLine(product);
                }
            }
            return products;
        }

        public async Task<ProductDto> GetProductsByCodeAsync(string productCode)
        {
            ProductDto product = new ProductDto();
            Console.WriteLine("Product to be updated " + product);

            using (_client)
            {
                var res = await _client.GetAsync("api/product/" + productCode);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    product = await res.Content.ReadAsAsync<ProductDto>();
                    Console.WriteLine(product);
                }
            }
            return product;
        }

        #endregion
    }
}
