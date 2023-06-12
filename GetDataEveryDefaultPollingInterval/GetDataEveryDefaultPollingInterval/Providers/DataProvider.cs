using GetDataEveryDefaultPollingInterval.Infra;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GetDataEveryDefaultPollingInterval.Providers
{
    public class DataProvider : GenericPoller
    {
        private const int DefaultPollingInterval = 2000;
        private const string Uri = "https://jsonplaceholder.typicode.com/todos/1";
        private readonly HttpClient _client = new();

        public void Start()
        {
            Start(DefaultPollingInterval, new CancellationToken());
        }

        protected override async Task GetDataAsync()
        {
            try
            {
                var response = await _client.GetAsync(Uri);
                if (response is null)
                {
                    Pulse();
                    return;
                }

                var result = await response.Content.ReadAsStringAsync();
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