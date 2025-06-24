using System.Text;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        private RequestDelegate _nextDelegate;

        public ContentMiddleware(RequestDelegate next) => _nextDelegate = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower() == "/middleware")
            {
                await context.Response.WriteAsync(
                    "This is from the content middleware", Encoding.UTF8);
            }
            else
            {
                await _nextDelegate.Invoke(context);
            }
        }
    }
}
