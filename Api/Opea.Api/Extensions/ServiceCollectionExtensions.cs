using Opea.Api.Middleware;

namespace Opea.Api.Extensions
{
    /// <summary>
    /// Extensões para configuração de serviços
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adiciona o middleware global de tratamento de exceções
        /// </summary>
        /// <param name="app">Builder da aplicação</param>
        /// <returns>Builder da aplicação</returns>
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }

        /// <summary>
        /// Adiciona o middleware de logging de requisições
        /// </summary>
        /// <param name="app">Builder da aplicação</param>
        /// <returns>Builder da aplicação</returns>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
