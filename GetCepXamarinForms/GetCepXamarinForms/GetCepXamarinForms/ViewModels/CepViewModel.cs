using GetCepXamarinForms.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetCepXamarinForms.ViewModels
{
    public class CepViewModel : BaseViewModel
    {
        private string _cepInputed;
        private bool _invalidCep;
        private bool _cepNotFound;
        private CepSchema cepSchema;

        public string CepInputed
        {
            get => _cepInputed;
            private set { SetProperty(ref _cepInputed, value); }
        }

        public bool InvalidCep 
        { 
            get => _invalidCep; 
            private set { SetProperty(ref _invalidCep, value); } 
        }

        public bool CepNotFound 
        { 
            get => _cepNotFound; 
            private set { SetProperty(ref _cepNotFound, value); } 
        }

        public CepSchema CepSchema 
        { 
            get => cepSchema; 
            private set { SetProperty(ref cepSchema, value); } 
        }

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
                var url = $"https://viacep.com.br/ws/{CepInputed}/json/";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
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
            catch (Exception) { throw; }
        }

        public void ResetProperties()
        {
            InvalidCep = false;
            CepNotFound = false;
            CepSchema = new CepSchema();
        }
    }
}