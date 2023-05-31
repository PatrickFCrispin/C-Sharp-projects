using System.Threading;
using System.Threading.Tasks;

namespace GetDataEveryDefaultPollingInterval.Infra
{
    public abstract class GenericPoller
    {
        private CancellationToken _cancellationToken;
        private readonly object _lock = new();
        private bool _isRunning;
        public int PollingIntervalMS { get; protected set; }

        public void Start(int pollingIntervalMs, CancellationToken cancellationToken)
        {
            PollingIntervalMS = pollingIntervalMs;

            lock (_lock)
            {
                if (_isRunning) return;

                _cancellationToken = cancellationToken;
                _isRunning = true;

                DelayThenPerformPollingAction();
            }
        }

        private void DelayThenPerformPollingAction()
        {
            Task.Delay(PollingIntervalMS).ContinueWith(async _ =>
            {
                if (!_isRunning) return;

                await GetData();
            }, _cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        protected abstract Task GetData();

        protected void Pulse()
        {
            if (!_isRunning) return;

            DelayThenPerformPollingAction();
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!_isRunning) return;

                _isRunning = false;
            }
        }
    }
}