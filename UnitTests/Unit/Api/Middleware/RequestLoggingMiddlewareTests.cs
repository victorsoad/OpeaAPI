using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opea.Api.Middleware;

namespace Opea.Tests.Unit.Api.Middleware
{
    /// <summary>
    /// Testes unitários para o RequestLoggingMiddleware
    /// </summary>
    public class RequestLoggingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<ILogger<RequestLoggingMiddleware>> _loggerMock;
        private readonly RequestLoggingMiddleware _middleware;

        public RequestLoggingMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _loggerMock = new Mock<ILogger<RequestLoggingMiddleware>>();
            _middleware = new RequestLoggingMiddleware(_nextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_ComRequisicaoValida_DeveLogarInicioEFim()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/test";
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");

            _nextMock.Setup(x => x(context)).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _nextMock.Verify(x => x(context), Times.Once);
            
            // Verifica se o RequestId foi adicionado ao contexto
            context.Items.Should().ContainKey("RequestId");
            context.Items["RequestId"].Should().NotBeNull();
        }

        [Fact]
        public async Task InvokeAsync_ComExcecao_DeveLogarInicioEFim()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.Path = "/api/test";
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");

            var exception = new Exception("Test exception");
            _nextMock.Setup(x => x(context)).ThrowsAsync(exception);

            // Act & Assert
            var action = async () => await _middleware.InvokeAsync(context);
            await action.Should().ThrowAsync<Exception>();
            
            // Verifica se o RequestId foi adicionado ao contexto mesmo com exceção
            context.Items.Should().ContainKey("RequestId");
            context.Items["RequestId"].Should().NotBeNull();
        }

        [Fact]
        public async Task InvokeAsync_DeveGerarRequestIdUnico()
        {
            // Arrange
            var context1 = new DefaultHttpContext();
            var context2 = new DefaultHttpContext();

            _nextMock.Setup(x => x(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context1);
            await _middleware.InvokeAsync(context2);

            // Assert
            var requestId1 = context1.Items["RequestId"]?.ToString();
            var requestId2 = context2.Items["RequestId"]?.ToString();

            requestId1.Should().NotBeNull();
            requestId2.Should().NotBeNull();
            requestId1.Should().NotBe(requestId2);
        }
    }
}
