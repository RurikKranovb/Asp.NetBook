using System.Text;

namespace ConfiguringApps.Infrastructure
{
    public class ErrorMiddleware
    {
        private RequestDelegate _nextDelegate;
        public ErrorMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            await _nextDelegate.Invoke(context);

            if (context.Response.StatusCode == 403)
            {
                await context.Response
                    .WriteAsync("Edge not supported", Encoding.UTF8);
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response
                    .WriteAsync("No content middleware response", Encoding.UTF8);
            }
        }
    }
}
