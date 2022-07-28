using GetDataEveryDefaultPollingInterval.Infra;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GetDataEveryDefaultPollingInterval.Providers
{
    public class DataProvider : GenericPoller
    {
        const int DefaultPollingInterval = 2000;
        const string uri = "https://jsonplaceholder.typicode.com/todos/1";
        readonly HttpClient client = new();

        public void Start()
        {
            Start(DefaultPollingInterval, new CancellationToken());
        }

        protected override async Task GetData()
        {
            try
            {
                var response = await client.GetAsync(uri);

                if (response == null)
                {
                    Pulse();
                    return;
                }

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Stop();
                return;
            }

            Pulse();
        }
    }
}