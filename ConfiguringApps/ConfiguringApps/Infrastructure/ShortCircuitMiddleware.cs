namespace ConfiguringApps.Infrastructure
{
    public class ShortCircuitMiddleware
    {
        private RequestDelegate _nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //if (context.Request.Headers["User-Agent"].Any(h => h.ToLower().Contains("edge")))
            if (context.Items["EdgeBrowser"] as bool? == true)
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await _nextDelegate.Invoke(context);
            }
        }
    }
}
