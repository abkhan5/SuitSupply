using System;
using System.Threading.Tasks;
using SuitSupply.ClientCommon;

namespace SuitSupply.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Service is "+SuitSupplyClient.ServicePath);
            var runMoreCommand = true;
            while (runMoreCommand)
            {
                var option = PrintOptions();
                if (option == 0)
                    break;

                try
                {
                    var client = new SuitSupplyClient();
                    var productTask = new Task(() => { });
                    switch (option)
                    {
                        case 1:
                            productTask = client.GetProductsAsync();
                            break;
                        case 2:
                            productTask = client.PostProduct();
                            break;
                        case 3:
                            productTask = client.UpdateProduct();
                            break;
                    }
                    productTask.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Failure : " + ex);
                }
            }
        }

        private static int PrintOptions()
        {
            Console.WriteLine("1. All Products");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Update Product");
            var resp = Console.ReadLine();
            if (string.IsNullOrEmpty(resp))
                return 0;
            var option = int.Parse(resp);
            return option;
        }
    }
}