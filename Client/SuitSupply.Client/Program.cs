using System;
using System.Threading.Tasks;
using SuitSupply.ClientCommon;

namespace SuitSupply.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var runMoreCommand = true;

            while (runMoreCommand)
            {
                var option = PrintOptions();
                if (option == 0)
                    break;

                switch (option)
                {
                    case 1:
                        ProductDetail.Product();
                        break;
                    case 2:
                        Sms();
                        break;
                }
            }
        }

        private static void Sms()
        {
            new SmsDetails();
        }

        private static int PrintOptions()
        {
            Console.WriteLine("1. Products");
            Console.WriteLine("2. Sms");
            var resp = Console.ReadLine();
            if (string.IsNullOrEmpty(resp))
                return 0;
            var option = int.Parse(resp);
            return option;
        }
    }
}