using System.Diagnostics;

namespace Opea.Api.Middleware
{
    /// <summary>
    /// Middleware para logging de requisições HTTP
    /// </summary>
    public class RequestLoggingMiddleware
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
            var stopwatch = Stopwatch.StartNew();
            var requestId = Guid.NewGuid().ToString("N")[..8];

            // Log da requisição
            _logger.LogInformation(
                "Iniciando requisição {RequestId}: {Method} {Path} de {RemoteIp}",
                requestId,
                context.Request.Method,
                context.Request.Path,
                context.Connection.RemoteIpAddress
            );

            // Adiciona o RequestId ao contexto para uso em outros middlewares
            context.Items["RequestId"] = requestId;

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Log da resposta
                _logger.LogInformation(
                    "Finalizando requisição {RequestId}: {Method} {Path} - Status: {StatusCode} - Tempo: {ElapsedMs}ms",
                    requestId,
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds
                );
            }
        }
    }
}
