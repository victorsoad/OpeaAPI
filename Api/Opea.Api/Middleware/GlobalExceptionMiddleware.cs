using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Opea.Api.Middleware
{
    /// <summary>
    /// Middleware global para tratamento de exceções
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado ocorreu: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails();

            switch (exception)
            {
                case ArgumentNullException nullEx:
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Title = "Parâmetro obrigatório não fornecido";
                    response.Detail = nullEx.Message;
                    break;

                case ArgumentException argEx:
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Title = "Erro de validação";
                    response.Detail = argEx.Message;
                    break;

                case InvalidOperationException opEx:
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Title = "Operação inválida";
                    response.Detail = opEx.Message;
                    break;

                case UnauthorizedAccessException:
                    response.Status = (int)HttpStatusCode.Unauthorized;
                    response.Title = "Acesso não autorizado";
                    response.Detail = "Você não tem permissão para realizar esta operação";
                    break;

                case KeyNotFoundException:
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Title = "Recurso não encontrado";
                    response.Detail = "O recurso solicitado não foi encontrado";
                    break;

                case TimeoutException:
                    response.Status = (int)HttpStatusCode.RequestTimeout;
                    response.Title = "Timeout da operação";
                    response.Detail = "A operação excedeu o tempo limite";
                    break;

                default:
                    response.Status = (int)HttpStatusCode.InternalServerError;
                    response.Title = "Erro interno do servidor";
                    response.Detail = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.";
                    break;
            }

            context.Response.StatusCode = response.Status ?? (int)HttpStatusCode.InternalServerError;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
