using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Infrastructure.Middlewares
{
    public sealed class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                // ��� ���� ��� �� ����� ���� ������� Body �� Request
                // context.Request.EnableBuffering();
                // ......
                //context.Request.Body.Position = 0;
                _logger.LogInformation($"Request headers: {String.Join(";", context.Request.Headers)}");
                _logger.LogInformation($"Route: {context.Request.Path}");
                _logger.LogInformation($"Response headers: {String.Join(";", context.Response.Headers)}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request and response");
            }
        }
    }
}