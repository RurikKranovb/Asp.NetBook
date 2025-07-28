using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class TimerFilter : IAsyncActionFilter, IAsyncResultFilter
    {

        private Stopwatch _timer;
        private IFilterDiagnostics _filterDiagnostics;

        public TimerFilter(IFilterDiagnostics diagnostics)
        {
            _filterDiagnostics = diagnostics;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _timer = Stopwatch.StartNew();

            await next();

            _filterDiagnostics.AddMessage($"Action time: {_timer.Elapsed.TotalMilliseconds}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
            _timer.Stop();
            
            _filterDiagnostics.AddMessage($"Result time: {_timer.Elapsed.TotalMilliseconds}");
        }
    }
}
