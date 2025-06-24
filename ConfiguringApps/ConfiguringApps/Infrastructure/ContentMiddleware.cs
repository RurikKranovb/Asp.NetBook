using System.Text;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        private  RequestDelegate _next;
        private  UpTimeService _up;
   

        public ContentMiddleware(RequestDelegate next, UpTimeService up)
        {
            _next = next;
            _up = up;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower() == "/middleware")
            {
                await context.Response.WriteAsync(
                    "This is from the content middleware" + $"(upTime: {_up.UpTime}ms)", Encoding.UTF8);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
