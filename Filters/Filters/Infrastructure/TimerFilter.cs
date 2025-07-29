using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class TimerFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private ConcurrentQueue<double> _actionTimes = new ConcurrentQueue<double>();
        private ConcurrentQueue<double> _resultTimes = new ConcurrentQueue<double>();


        //private Stopwatch _timer;
        private IFilterDiagnostics _filterDiagnostics;

        public TimerFilter(IFilterDiagnostics diagnostics)
        {
            _filterDiagnostics = diagnostics;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var _timer = Stopwatch.StartNew();

            await next();

            _timer.Stop();
            _actionTimes.Enqueue(_timer.Elapsed.TotalMilliseconds);

            _filterDiagnostics.AddMessage($"Action time: {_timer.Elapsed.TotalMilliseconds} Average: {_actionTimes.Average():F2}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var _timer = Stopwatch.StartNew();

            await next();
            _timer.Stop();

            _resultTimes.Enqueue(_timer.Elapsed.TotalMilliseconds);
            
            _filterDiagnostics.AddMessage($"Result time: {_timer.Elapsed.TotalMilliseconds}  Average: {_actionTimes.Average():F2}");
        }
    }
}
