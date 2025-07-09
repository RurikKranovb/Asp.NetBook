using System.Text;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : IRouter
    {
        private string[] _urls;

        public LegacyRoute(params string[] targetUrls)
        {
            _urls = targetUrls;
        }
        public Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path
                .Value.TrimEnd('/');

            if (_urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    HttpResponse response = ctx.Response;
                    byte[] bytes = Encoding.ASCII.GetBytes($"URL: {requestedUrl}");
                    await response.Body.WriteAsync(bytes, 0, bytes.Length);
                };
            }
            return Task.CompletedTask;
        }

        public VirtualPathData? GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
