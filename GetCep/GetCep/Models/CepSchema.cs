using Newtonsoft.Json;
using System;

namespace GetCep.Models
{
    public class CepSchema
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public int? Ddd { get; set; }
        public string Siafi { get; set; }

        public static void UpdateCep(string content)
        {
            var cepSchema = JsonConvert.DeserializeObject<CepSchema>(content);
            if (string.IsNullOrWhiteSpace(cepSchema.Cep))
            {
                Console.WriteLine("Cep não encontrado.");
                return;
            }

            Console.WriteLine("*** Resultado ***");
            Console.WriteLine($"Cep: {cepSchema.Cep}");
            Console.WriteLine($"Logradouro: {cepSchema.Logradouro}");
            Console.WriteLine($"Complemento: {cepSchema.Complemento}");
            Console.WriteLine($"Bairro: {cepSchema.Bairro}");
            Console.WriteLine($"Localidade: {cepSchema.Localidade}");
            Console.WriteLine($"Uf: {cepSchema.Uf}");
            Console.WriteLine($"Ibge: {cepSchema.Ibge}");
            Console.WriteLine($"Gia: {cepSchema.Gia}");
            Console.WriteLine($"Ddd: {cepSchema.Ddd}");
            Console.WriteLine($"Siafi: {cepSchema.Siafi}");
        }
    }
}