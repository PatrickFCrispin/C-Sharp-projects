using DetalhesAtivo.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DetalhesAtivo.Models.SnapshotResponse;

namespace DetalhesAtivo.Providers
{
    public class SecurityProvider
    {
        Security security;

        public SecurityProvider()
        {
            security = new Security();
        }

        public async Task<Security> GetSecurityDataForAsync(string symbol)
        {
            try
            {
                var client = new HttpClient();
                string uri = $"http://demo.intelitrader.com.br:5200/iwg/snapshot?t=webgateway&minify=false&q={symbol},1";
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    security = JsonConvert
                        .DeserializeObject<SnapshotResponse>(content)
                        .Value
                        .FirstOrDefault();
                }
                else
                {
                    security = null;
                }
            }
            catch (Exception ex)
            {
                security = null;
                throw ex;
            }

            return security;
        }
    }
}