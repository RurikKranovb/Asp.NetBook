using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class ProfileAttribute : ActionFilterAttribute
    {
        //private Stopwatch _timer;

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    _timer = Stopwatch.StartNew();
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    _timer.Stop();
        //    string result = $"<div>Elapsed time: {_timer.Elapsed.TotalMilliseconds} ms<div>";

        //    byte[] bytes = Encoding.ASCII.GetBytes(result);
        //    context.HttpContext.Response.Body.WriteAsync(bytes,0,bytes.Length);
        //}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch timer = Stopwatch.StartNew();
            await next();

            timer.Stop();
            string result = $"<div>Elapsed time: {timer.Elapsed.TotalMilliseconds} ms<div>";

            byte[] bytes = Encoding.ASCII.GetBytes(result);
            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);


        }
    }
}
