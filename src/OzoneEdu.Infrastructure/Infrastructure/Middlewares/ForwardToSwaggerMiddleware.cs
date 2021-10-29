using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Infrastructure.Middlewares
{
    public sealed class ForwardToSwaggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ForwardToSwaggerMiddleware> _logger;

        public ForwardToSwaggerMiddleware(RequestDelegate next, ILogger<ForwardToSwaggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await Redirect(context);
        }

        private async Task Redirect(HttpContext context)
        {
            try
            {
                await Task.Run(() => context.Response.Redirect("swagger"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}