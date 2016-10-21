#region Namespace
using System;
using System.Threading.Tasks;
using SuitSupply.ClientCommon;

#endregion
namespace SuitSupply.ConsoleClient
{
  internal  class SmsDetails
    {
        private int PrintOptions()
        {
            Console.WriteLine("1. Get Countries");
            Console.WriteLine("2. Send SMS");
            Console.WriteLine("3. Get Sent Sms");
            Console.WriteLine("4. Get Statistics");

            var resp = Console.ReadLine();
            if (string.IsNullOrEmpty(resp))
                return 0;
            var option = int.Parse(resp);
            return option;
        }
        public SmsDetails()
        {
            Console.WriteLine("Service is " + SuitSupplyClient.ServicePath);
            var runMoreCommand = true;
            while (runMoreCommand)
            {
                var option = PrintOptions();
                if (option == 0)
                    break;

                try
                {
                    var client = new SmsClient();
                    var smsTask = new Task(() => { });
                    switch (option)
                    {
                        case 1:
                            smsTask = client.GetCountries();
                            break;
                        case 2:
                            smsTask = client.SendMessage();
                            break;
                        case 3:
                            //smsTask = client.UpdateProduct();
                            break;
                    }
                    smsTask.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Failure : " + ex);
                }
            }
        }
    }
}
