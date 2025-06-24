namespace ConfiguringApps.Infrastructure
{
    public class BrowserTypeMiddleware
    {
        private RequestDelegate _nextDelegate;
        public BrowserTypeMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items["EdgeBrowser"]
                = context.Request.Headers["User-Agent"]
                    .Any(v => v.ToLower().Contains("edge"));
            await _nextDelegate.Invoke(context);
        }
    }
}
