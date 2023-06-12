using GetCep.Controllers;
using System.Threading.Tasks;

namespace GetCep
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cepController = new CepController();
            await cepController.ConnectToApiAndGetCepContentAsync();
        }
    }
}