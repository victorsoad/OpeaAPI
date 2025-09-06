using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Opea.Api.Middleware;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Opea.Tests.Unit.Api.Middleware
{
    /// <summary>
    /// Testes unitários para o GlobalExceptionMiddleware
    /// </summary>
    public class GlobalExceptionMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<ILogger<GlobalExceptionMiddleware>> _loggerMock;
        private readonly GlobalExceptionMiddleware _middleware;

        public GlobalExceptionMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();
            _middleware = new GlobalExceptionMiddleware(_nextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_ComExcecaoArgumentException_DeveRetornarBadRequest()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new ArgumentException("Parâmetro inválido");

            _nextMock.Setup(x => x(context)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            context.Response.ContentType.Should().Be("application/json");

            var responseBody = await GetResponseBody(context.Response);
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody);
            
            problemDetails.Should().NotBeNull();
            problemDetails!.Status.Should().Be((int)HttpStatusCode.BadRequest);
            problemDetails.Title.Should().Be("Erro de validação");
            problemDetails.Detail.Should().Be("Parâmetro inválido");
        }

        [Fact]
        public async Task InvokeAsync_ComExcecaoKeyNotFoundException_DeveRetornarNotFound()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new KeyNotFoundException("Recurso não encontrado");

            _nextMock.Setup(x => x(context)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            context.Response.ContentType.Should().Be("application/json");

            var responseBody = await GetResponseBody(context.Response);
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody);
            
            problemDetails.Should().NotBeNull();
            problemDetails!.Status.Should().Be((int)HttpStatusCode.NotFound);
            problemDetails.Title.Should().Be("Recurso não encontrado");
        }

        [Fact]
        public async Task InvokeAsync_ComExcecaoGenerica_DeveRetornarInternalServerError()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new Exception("Erro genérico");

            _nextMock.Setup(x => x(context)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            context.Response.ContentType.Should().Be("application/json");

            var responseBody = await GetResponseBody(context.Response);
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody);
            
            problemDetails.Should().NotBeNull();
            problemDetails!.Status.Should().Be((int)HttpStatusCode.InternalServerError);
            problemDetails.Title.Should().Be("Erro interno do servidor");
        }

        [Fact]
        public async Task InvokeAsync_SemExcecao_DeveContinuarPipeline()
        {
            // Arrange
            var context = new DefaultHttpContext();

            _nextMock.Setup(x => x(context)).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _nextMock.Verify(x => x(context), Times.Once);
        }

        private static async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Position = 0;
            using var reader = new StreamReader(response.Body);
            return await reader.ReadToEndAsync();
        }
    }
}
