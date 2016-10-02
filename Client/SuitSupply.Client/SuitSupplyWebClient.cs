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
            var product = ProductSampleData.GetSampleDto();
            CreateProductAsync(product);
        }

         HttpClient client = new HttpClient();
        public const string ServicePath = "http://localhost/SuitSupply.Server.ServiceHost/api/product/";
      

         async Task<Uri> CreateProductAsync(ProductDto product)
         {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();


            // return URI of the created resource.
            return response.Headers.Location;
        }

        async Task<ProductDto> GetProductAsync(string path)
        {
            ProductDto product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<ProductDto>();
            }
            return product;
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
