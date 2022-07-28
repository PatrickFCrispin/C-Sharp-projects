using System.Threading;
using System.Threading.Tasks;

namespace GetDataEveryDefaultPollingInterval.Infra
{
    public abstract class GenericPoller
    {
        CancellationToken _cancellationToken;
        readonly object _lock = new();
        bool isRunning;
        public int PollingIntervalMS { get; protected set; }

        public void Start(int pollingIntervalMs, CancellationToken cancellationToken)
        {
            PollingIntervalMS = pollingIntervalMs;

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
            Task.Delay(PollingIntervalMS).ContinueWith(async _ =>
            {
                if (!isRunning) return;

                await GetData();
            }, _cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        protected abstract Task GetData();

        protected void Pulse()
        {
            if (!isRunning) return;

            DelayThenPerformPollingAction();
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!isRunning) return;

                isRunning = false;
            }
        }
    }
}