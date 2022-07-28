using GetCepXamarinForms.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetCepXamarinForms.ViewModels
{
    public class CepViewModel : BaseViewModel
    {
        public string CepInputed { get; set; }

        bool invalidCep;
        public bool InvalidCep { get => invalidCep; set { SetProperty(ref invalidCep, value); } }

        bool cepNotFound;
        public bool CepNotFound { get => cepNotFound; set { SetProperty(ref cepNotFound, value); } }

        CepSchema cepSchema;
        public CepSchema CepSchema { get => cepSchema; set { SetProperty(ref cepSchema, value); } }

        public CepViewModel()
        {
            CepSchema = new CepSchema();
        }

        public async Task UpdateCepAsync()
        {
            if (CepInputed.Length < 8)
            {
                InvalidCep = true;
                return;
            }

            try
            {
                var httpClient = new HttpClient();
                string url = $"https://viacep.com.br/ws/{CepInputed}/json/";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var cepSchema = JsonConvert.DeserializeObject<CepSchema>(content);

                    if (string.IsNullOrEmpty(cepSchema.Cep))
                    {
                        CepNotFound = true;
                        return;
                    }

                    CepNotFound = false;
                    CepSchema = cepSchema;
                }
            } 
            catch { }
        }
    }
}