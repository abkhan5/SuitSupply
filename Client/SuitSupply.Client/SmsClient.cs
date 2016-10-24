using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.DataContracts;

namespace SuitSupply.ConsoleClient
{
    public  class SmsClient
    {
        private const string LocalExpressServicePath = "http://localhost/SuitSupply.Server.ServiceHost/";
        private const string LocalServicePath = "http://localhost/SuitSupply.Server.ServiceHost/";
        private const string AzureServicePath = "http://appversewebapi.azurewebsites.net/";
        private const string ControllerPath = "api/MittoSms";
        public const string ServicePath = AzureServicePath;

        private HttpClient _client;

        public SmsClient()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServicePath) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<CountryPackages>> GetCountries()
        {
            IEnumerable<CountryPackages> countries=null;
            using (_client)
            {
                var res = await _client.GetAsync(ControllerPath);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    countries = await res.Content.ReadAsAsync<IEnumerable<CountryPackages>>();
                    foreach (var country in countries)
                    {
                        Console.WriteLine(country);
                    }
                }
            }
            return countries;
        }


        #region Namespace

        public async Task SendMessage()
        {
            var sms = new SmsRequest();
            sms.From = "496602289916";
            sms.To= "496604924771";
            sms.Text = "this is a sample text to add " + DateTime.Now;
            await CreateSmsAsync(sms);
        }

        private async Task<Uri> CreateSmsAsync(SmsRequest sms)
        {
            var response = await _client.PostAsJsonAsync(ControllerPath, sms);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var result = await  response.Content.ReadAsAsync<MessageStateEnum>();
                if (result == MessageStateEnum.Sent)
                {
                    Console.WriteLine(" Sms successfully added");
                }
                else
                {
                    Console.WriteLine(" Sms add failed");
                }

            }
            else
            {
                Console.WriteLine(" Sms add failed");
            }
            // return URI of the created resource.
            return response.Headers.Location;
        }
        #endregion
    }
}
