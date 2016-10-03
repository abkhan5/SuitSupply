using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SuitSupply.DataObject;

namespace SuitSupply.Client
{
     class SuitSupplyWebClient
    {
        public SuitSupplyWebClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ServicePath);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
         }

        private readonly HttpClient _client ;
        private const string ServicePath = "http://localhost/SuitSupply.Server.ServiceHost/";
        private const string AzureServicePath = "http://suitsupplywebapi.azurewebsites.net/";

        public async Task PostProduct()
        {
            var product = ProductSampleData.GetSampleDto();
            await CreateProductAsync(product);
        }

        async Task<Uri> CreateProductAsync(ProductDto product)
         {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/product", product);
            response.EnsureSuccessStatusCode();


            // return URI of the created resource.
            return response.Headers.Location;
        }

      public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            IEnumerable<ProductDto> products = new List<ProductDto>();
            using (_client)
            {
                HttpResponseMessage res = await _client.GetAsync("api/product");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    products = await res.Content.ReadAsAsync<IEnumerable<ProductDto>>();
                    foreach (var product in products)
                    {
                        Console.WriteLine(product);
                     
                    }
                }
            }
            return products;
        }
        //async Task RunAsync()
        //{
        //    client.BaseAddress = new Uri(ServicePath);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {
        //        // Create a new product
        //        ProductDto product = new ProductDto { ProductName = "Gizmo" };

        //        var url = await CreateProductAsync(product);
        //        Console.WriteLine($"Created at {url}");

        //        // Get the product
        //        product = await GetProductAsync(url.PathAndQuery);
        //        ShowProduct(product);

        //        // Update the product
        //        Console.WriteLine("Updating price...");
        //        product.Price = 80;
        //        await UpdateProductAsync(product);

        //        // Get the updated product
        //        product = await GetProductAsync(url.PathAndQuery);
        //        ShowProduct(product);

        //        // Delete the product
        //        var statusCode = await DeleteProductAsync(product.Id);
        //        Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    Console.ReadLine();
        //}
         void ShowProduct(ProductDto product)
        {
            Console.WriteLine($"Name: {product.ProductName}");
        }

    }
}
