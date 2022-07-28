using GetCep.Controllers;
using System.Threading.Tasks;

namespace GetCep
{
    class Program
    {
        static Task Main(string[] args)
        {
            var cepController = new CepController();
            return cepController.ConnectToApiAndGetCepContent();
        }
    }
}