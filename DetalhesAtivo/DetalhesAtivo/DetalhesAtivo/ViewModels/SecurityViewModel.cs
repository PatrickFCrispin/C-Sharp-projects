using DetalhesAtivo.Providers;
using System.Threading.Tasks;
using static DetalhesAtivo.Models.SnapshotResponse;

namespace DetalhesAtivo.ViewModels
{
    public class SecurityViewModel : BaseViewModel
    {
        readonly SecurityProvider _securityProvider;

        public string Symbol { get; set; }

        Security security;
        public Security Security { get => security; set { SetProperty(ref security, value); } }

        public SecurityViewModel()
        {
            _securityProvider = new SecurityProvider();
            Security = new Security();
        }

        public async Task UpdateSecurityAsync()
        {
            Security = await _securityProvider.GetSecurityDataForAsync(Symbol);
        }
    }
}