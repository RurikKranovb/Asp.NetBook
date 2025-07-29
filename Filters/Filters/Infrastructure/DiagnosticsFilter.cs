using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class DiagnosticsFilter : IAsyncResourceFilter
    {
        private IFilterDiagnostics _diagnostics;

        public DiagnosticsFilter(IFilterDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            await next();

            foreach (var diagnosticsMessage in _diagnostics?.Messages)
            {
                byte[] bytes = Encoding.ASCII.GetBytes($"<div>{diagnosticsMessage}</div>");

                await context.HttpContext.Response.Body
                    .WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
