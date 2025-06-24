using System.Diagnostics;

namespace ConfiguringApps.Infrastructure
{
    public class UpTimeService
    {
        private Stopwatch _timer;

        public UpTimeService()
        {
            _timer = Stopwatch.StartNew();
            
        }

        public long UpTime => _timer.ElapsedMilliseconds;

    }
}
