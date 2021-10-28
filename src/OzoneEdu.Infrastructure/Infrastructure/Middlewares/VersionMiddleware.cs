using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace OzonEdu.Infrastructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetEntryAssembly().GetName().Version?.ToString() ?? "no version";
            var name = Assembly.GetEntryAssembly().GetName().Name;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { version = version, serviceName = name }));
        }
    }
}