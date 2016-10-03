using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataObject;

namespace SuitSupply.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var runMoreCommand = true;
            while (runMoreCommand)
            {
                var option= PrintOptions();
                if (option==0)
                {
                    break;
                }

                try
                {
                    var client = new SuitSupplyWebClient();
                    Task productTask = new Task(() => { });
                    switch (option)
                    {
                        case 1:
                            productTask = client.GetProductsAsync();
                            break;
                        case 2:
                            productTask = client.PostProduct();
                            break;
                    }
                    productTask.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Failure : "+ex);
                }  
            }
        }

      static int PrintOptions()
        {
            Console.WriteLine("1. All Products");
            Console.WriteLine("2. Add Product");
            var resp = Console.ReadLine();
            if (string.IsNullOrEmpty(resp))
            {
                return 0;
            }
            var option = int.Parse(resp);
            return option;
        }
      
    }
}
