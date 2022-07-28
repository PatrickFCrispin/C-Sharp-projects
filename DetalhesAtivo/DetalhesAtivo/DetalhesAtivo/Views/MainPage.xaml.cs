using DetalhesAtivo.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DetalhesAtivo.Views
{
    public partial class MainPage : ContentPage
    {
        SecurityViewModel _viewModel;
        readonly object _lock = new object();
        bool isRunning;
        CancellationToken _cancellationToken;
        const int DefaultPollingInterval = 2000;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SecurityViewModel();
        }

        void Start(CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                if (isRunning) return;

                _cancellationToken = cancellationToken;
                isRunning = true;

                DelayThenPerformPollingAction();
            }
        }

        void DelayThenPerformPollingAction()
        {
            Task.Delay(DefaultPollingInterval).ContinueWith(async _ =>
            {
                if (!isRunning) return;

                await _viewModel.UpdateSecurityAsync();
                Pulse();
            }, _cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        void Pulse()
        {
            if (!isRunning) return;

            DelayThenPerformPollingAction();
        }

        void Stop()
        {
            lock (_lock)
            {
                if (!isRunning) return;

                isRunning = false;
            }
        }

        void StartTimerAndUpdateSecurity(object sender, EventArgs e)
        {
            Start(new CancellationToken());
        }

        void StopTimerAndClearSecurity(object sender, TextChangedEventArgs e)
        {
            _viewModel.Security = null;
            Stop();
        }
    }
}