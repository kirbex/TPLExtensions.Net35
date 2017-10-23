namespace TPLExtensions.Net35
{
    using System;
    using System.Threading;

    using Timer = System.Timers.Timer;

    public sealed class CancellationTokenSourceWithTimeout : IDisposable
    {
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private readonly Timer timer = new Timer();

        public CancellationTokenSourceWithTimeout(int timeout)
            : this(TimeSpan.FromMilliseconds(timeout))
        {
        }

        public CancellationTokenSourceWithTimeout(TimeSpan timeout)
        {
            Token = cts.Token;
            timer.Interval = timeout.TotalMilliseconds;
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = false;
            timer.Start();
        }

        public CancellationToken Token { get; }

        public void Dispose()
        {
            timer.Stop();
            cts?.Dispose();
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                cts.Cancel();
            }
            catch (ObjectDisposedException)
            { }
        }
    }
}
