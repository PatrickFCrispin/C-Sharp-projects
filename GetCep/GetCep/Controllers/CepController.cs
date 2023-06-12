using GetCep.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetCep.Controllers
{
    public class CepController
    {
        // Altere o cep para realizar a pesquisa. Valores aceitos: 93180000 | 93180-000
        private readonly string Cep = "01001000";

        public async Task ConnectToApiAndGetCepContentAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var url = $"https://viacep.com.br/ws/{Cep}/json";
                var responseMessage = await httpClient.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    CepSchema.UpdateCep(content);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}